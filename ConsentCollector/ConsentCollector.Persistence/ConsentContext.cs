using ConsentCollector.Entities.Consent;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=DESKTOP-B7LDT20\SQLEXPRESS;Initial Catalog=Consent;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Username)
                .HasColumnName("Username")
                .HasMaxLength(20)
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Password)
                .HasColumnName("Password")
                .HasMaxLength(20)
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Role)
                .HasColumnName("Role")
                .IsRequired()
                .ValueGeneratedNever();

                e.HasOne(u => u.Detail)
                .WithOne(d => d.User)
                .HasForeignKey<UserDetail>(d=>d.IdUser);
            });

            modelBuilder.Entity<Survey>(e =>
            {
                e.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedNever();


                e.Property(c => c.Subject)
               .HasColumnName("Subject")
               .IsRequired()
               .ValueGeneratedNever();

                e.Property(c => c.Description)
               .HasColumnName("Description")
               .IsRequired()
               .ValueGeneratedNever();

                e.Property(c => c.LegalBasis)
               .HasColumnName("LegalBasis")
               .IsRequired()
               .ValueGeneratedNever();

                e.Property(c => c.LaunchDate)
               .HasColumnName("LaunchDate")
               .IsRequired()
               .ValueGeneratedNever();

                e.Property(c => c.ExpirationDate)
               .HasColumnName("ExpirationDate")
               .IsRequired()
               .ValueGeneratedNever();

            });


            modelBuilder.Entity<UserDetail>(e =>
            {
                e.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Firstname)
                .HasColumnName("Firstname")
                .HasMaxLength(20)
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Lastname)
                .HasColumnName("Lastname")
                .HasMaxLength(20)
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Number)
                .HasColumnName("Number")
                .HasMaxLength(10)
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Email)
                .HasColumnName("Email")
                .IsRequired()
                .ValueGeneratedNever();
            });
        }

        public async Task<IEnumerable<User>> GetUser()
        {
            return await User.Include(u=>u.Detail).ToListAsync();
        }
    }
}
