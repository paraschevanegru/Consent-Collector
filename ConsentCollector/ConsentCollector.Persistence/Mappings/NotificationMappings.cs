using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;

namespace ConsentCollector.Persistence.Mappings
{
    internal abstract class NotificationMappings
    {
        internal static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>(e =>
            {
                e.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Title)
                .HasColumnName("Title")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Description)
                .HasColumnName("Description")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Seen)
                    .HasColumnName("Seen")
                    .IsRequired()
                    .ValueGeneratedNever();
            });
        }
    }
}
