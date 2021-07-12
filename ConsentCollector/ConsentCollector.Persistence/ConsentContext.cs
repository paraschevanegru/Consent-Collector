using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public class ConsentContext:DbContext
    {
        public ConsentContext()
        {

        }
        public ConsentContext(DbContextOptions<ConsentContext> options):base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Question> Question { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=DESKTOP-B7LDT20\SQLEXPRESS;Initial Catalog=Consent;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AnswerMappings.Map(modelBuilder);
            CommentMappings.Map(modelBuilder);
            NotificationMappings.Map(modelBuilder);
            QuestionMappings.Map(modelBuilder);
            SurveyMappings.Map(modelBuilder);
            UserDetailMappings.Map(modelBuilder);
            UserMappings.Map(modelBuilder);

            //modelBuilder.Entity<User>(e =>
            //{
            //    e.Property(c => c.Id)
            //    .HasColumnName("Id")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Username)
            //    .HasColumnName("Username")
            //    .HasMaxLength(20)
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Password)
            //    .HasColumnName("Password")
            //    .HasMaxLength(20)
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Role)
            //    .HasColumnName("Role")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.HasOne(u => u.Detail)
            //    .WithOne(d => d.User)
            //    .HasForeignKey<UserDetail>(d=>d.IdUser);

            //    e.HasMany(u => u.Comments)
            //    .WithOne(c => c.User)
            //    .HasForeignKey(u => u.IdUser);

            //    e.HasMany(u => u.Answers)
            //    .WithOne(a => a.User)
            //    .HasForeignKey(u => u.IdUser);

            //    e.HasMany(u => u.Notifications)
            //     .WithOne(n => n.User)
            //     .HasForeignKey(u => u.IdUser);

            //});

            //modelBuilder.Entity<Survey>(e =>
            //{
            //    e.Property(c => c.Id)
            //    .HasColumnName("Id")
            //    .IsRequired()
            //    .ValueGeneratedNever();


            //    e.Property(c => c.Subject)
            //   .HasColumnName("Subject")
            //   .IsRequired()
            //   .ValueGeneratedNever();

            //    e.Property(c => c.Description)
            //   .HasColumnName("Description")
            //   .IsRequired()
            //   .ValueGeneratedNever();

            //    e.Property(c => c.LegalBasis)
            //   .HasColumnName("LegalBasis")
            //   .IsRequired()
            //   .ValueGeneratedNever();

            //    e.Property(c => c.LaunchDate)
            //   .HasColumnName("LaunchDate")
            //   .IsRequired()
            //   .ValueGeneratedNever();

            //    e.Property(c => c.ExpirationDate)
            //   .HasColumnName("ExpirationDate")
            //   .IsRequired()
            //   .ValueGeneratedNever();

            //    e.HasMany(s => s.Comments)
            //    .WithOne(c => c.Survey)
            //    .HasForeignKey(s => s.IdSurvey);

            //    e.HasMany(s => s.Answers)
            //  .WithOne(a => a.Survey)
            //  .HasForeignKey(s => s.IdSurvey);

            //    e.HasMany(s => s.Notifications)
            //     .WithOne(n => n.Survey)
            //     .HasForeignKey(s => s.IdSurvey);

            //});


            //modelBuilder.Entity<UserDetail>(e =>
            //{
            //    e.Property(c => c.Id)
            //    .HasColumnName("Id")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Firstname)
            //    .HasColumnName("Firstname")
            //    .HasMaxLength(20)
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Lastname)
            //    .HasColumnName("Lastname")
            //    .HasMaxLength(20)
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Number)
            //    .HasColumnName("Number")
            //    .HasMaxLength(10)
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Email)
            //    .HasColumnName("Email")
            //    .IsRequired()
            //    .ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Comment>(e =>
            //{
            //    e.Property(c => c.Id)
            //    .HasColumnName("Id")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Text)
            //    .HasColumnName("Text")
            //    .IsRequired()
            //    .ValueGeneratedNever();
            //});


            //modelBuilder.Entity<Answer>(e =>
            //{
            //    e.Property(c => c.Id)
            //    .HasColumnName("Id")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Agree)
            //    .HasColumnName("Agree")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.AnswerDate)
            //    .HasColumnName("AnswerDate")
            //    .IsRequired()
            //    .ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Notification>(e =>
            //{
            //    e.Property(c => c.Id)
            //    .HasColumnName("Id")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Title)
            //    .HasColumnName("Title")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Description)
            //    .HasColumnName("Description")
            //    .IsRequired()
            //    .ValueGeneratedNever();
            //});


            //modelBuilder.Entity<Question>(e =>
            //{
            //    e.Property(c => c.Id)
            //    .HasColumnName("Id")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Optional)
            //    .HasColumnName("Optional")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.Property(c => c.Questions)
            //    .HasColumnName("Questions")
            //    .IsRequired()
            //    .ValueGeneratedNever();

            //    e.HasMany(q => q.Answers)
            //    .WithOne(a => a.Question)
            //    .HasForeignKey(q => q.IdQuestion);
            //});
        }

        public async Task<IEnumerable<User>> GetUser()
        {
            return await User.Include(u=>u.Detail).ToListAsync();
        }
    }
}
