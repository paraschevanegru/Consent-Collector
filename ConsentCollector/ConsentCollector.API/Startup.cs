using ConsentCollector.Business;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Models.UserDetails;
using ConsentCollector.Business.Consent.Models.Users;
using ConsentCollector.Business.Consent.Services;
using ConsentCollector.Business.Consent.Services.CommentService;
using ConsentCollector.Business.Consent.Services.HistoryService;
using ConsentCollector.Business.Consent.Services.QuestionService;
using ConsentCollector.Business.Consent.Services.SurveyQuestionService;
using ConsentCollector.Business.Consent.Services.UserDetails;
using ConsentCollector.Business.Consent.Services.Users;
using ConsentCollector.Business.Consent.Validators;
using ConsentCollector.Persistence;
using ConsentCollector.Persistence.CommentRepository;
using ConsentCollector.Persistence.HistoryRepository;
using ConsentCollector.Persistence.QuestionRepository;
using ConsentCollector.Persistence.SurveyQuestionRepository;
using ConsentCollector.Persistence.UserRepository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace ConsentCollector.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("*")
                            .WithHeaders(HeaderNames.ContentType,
                                HeaderNames.AccessControlAllowMethods, 
                                HeaderNames.AccessControlAllowHeaders,
                                HeaderNames.AccessControlAllowOrigin );
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConsentCollector.API", Version = "v1" });
            });

            services.AddDbContext<ConsentContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("ConsentConnection"));
            });

            services.AddAutoMapper(config =>
            {
                config.AddProfile<ConsentMappingProfile>();
            });

            services.AddScoped<IConsentRepository, ConsentRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IHistoryService, HistoryService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserDetailRepository, UserDetailRepository>();
            services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDetailService, UserDetailService>();
            services.AddScoped<ISurveyQuestionService, SurveyQuestionService>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();

            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IAnswerService, AnswerService>();

            services.AddScoped<IValidator<CreateUserDetailModel>,CreateUserDetailModelValidator>();
            services.AddScoped<IValidator<CreateUserModel>, CreateUserModelValidator>();
            services.AddScoped<IValidator<CreateSurveyModel>, CreateSurveyModelValidator>();
            services.AddScoped<IValidator<CreateAnswerModel>, CreateAnswerModelValidator>();
            services.AddScoped<IValidator<NotificationModel>,CreateNotificationModelValidator>();
            services.AddSwaggerGen();

            services.AddMvc();
            services
                .AddControllersWithViews()
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConsentCollector.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors(MyAllowSpecificOrigins);
            app.UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireCors(MyAllowSpecificOrigins);
            });

            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<ConsentContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}
