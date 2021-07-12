using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.Mappings
{
    internal abstract class UserDetailMappings
    {
        internal static void Map(ModelBuilder modelBuilder)
        {
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
    }
}
