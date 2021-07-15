using ConsentCollector.Business;
using ConsentCollector.Business.Consent.Services;
using ConsentCollector.Business.Consent.Services.CommentService;
using ConsentCollector.Business.Consent.Services.QuestionService;
using ConsentCollector.Persistence;
using ConsentCollector.Persistence.CommentRepository;
using ConsentCollector.Persistence.QuestionRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Services.UserDetails;
using ConsentCollector.Business.Consent.Services.Users;
using ConsentCollector.Persistence.UserRepository;

namespace ConsentCollector.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
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

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserDetailRepository, UserDetailRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDetailService, UserDetailService>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();

            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IAnswerService, AnswerService>();

            services.AddSwaggerGen();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<ConsentContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}
