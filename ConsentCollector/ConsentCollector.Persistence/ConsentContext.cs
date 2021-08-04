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
        public DbSet<History> History { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=DESKTOP-B7LDT20\SQLEXPRESS;Initial Catalog=Consent;Integrated Security=True";
            //string connectionString = @"Data Source=DESKTOP-VM7UR1F\SQLEXPRESS;Initial Catalog=Consent;Integrated Security=True";
            //string connectionString = @"Data Source=DESKTOP-ML4HKD4\SQLEXPRESS;Initial Catalog=Consent;Integrated Security=True";

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
            SurveyQuestionMappings.Map(modelBuilder);
        }
          

        public async Task<IEnumerable<User>> GetUser()
        {
            return await User.Include(u=>u.Detail).ToListAsync();
        }
    }
}
