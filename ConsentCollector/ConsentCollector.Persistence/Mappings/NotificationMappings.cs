using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            });
        }
    }
}
