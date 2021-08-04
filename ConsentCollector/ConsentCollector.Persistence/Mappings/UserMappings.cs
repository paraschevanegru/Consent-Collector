using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;

namespace ConsentCollector.Persistence.Mappings
{
    internal abstract class UserMappings
    {
        internal static void Map(ModelBuilder modelBuilder)
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
                .HasMaxLength(2000)
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Role)
                .HasColumnName("Role")
                .IsRequired()
                .ValueGeneratedNever();

                e.HasCheckConstraint("CK_User_Role", "[Role] = 'admin' or [Role] = 'user'");

                e.HasIndex(c => c.Username)
                    .IsUnique(true);

                e.HasOne(u => u.Detail)
                .WithOne(d => d.User)
                .HasForeignKey<UserDetail>(d => d.IdUser);

                e.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(u => u.IdUser);

                e.HasMany(u => u.Answers)
                .WithOne(a => a.User)
                .HasForeignKey(u => u.IdUser);

                e.HasMany(u => u.Notifications)
                 .WithOne(n => n.User)
                 .HasForeignKey(u => u.IdUser);
            });
        }
    }
}
