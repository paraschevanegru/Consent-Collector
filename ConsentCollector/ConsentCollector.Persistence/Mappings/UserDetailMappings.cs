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

                e.HasCheckConstraint("CK_UserDetail_Firstname", "Len([Firstname])>=3 and Len([Firstname])<=20");

                e.Property(c => c.Lastname)
                .HasColumnName("Lastname")
                .HasMaxLength(20)
                .IsRequired()
                .ValueGeneratedNever();

                e.HasCheckConstraint("CK_UserDetail_Lastname", "Len([Lastname])>=3 and Len([Lastname])<=20");

                e.Property(c => c.Number)
                    .HasColumnName("Number")
                    .HasMaxLength(10)
                    .IsRequired()
                    .ValueGeneratedNever();

                //e.HasCheckConstraint("CK_UserDetail_Number", "Len([Number])>=10 and Len([Number])<=10");

                e.Property(c => c.Email)
                .HasColumnName("Email")
                .IsRequired()
                .ValueGeneratedNever();

                //e.HasCheckConstraint("CK_UserDetail_Email", @"[dbo].[REGEXP_LIKE] ([Email],'^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$')");

                e.HasIndex(c => c.Email)
                    .IsUnique(true);

                e.HasIndex(c => c.Number)
                    .IsUnique(true);
            });

        }
    }
}
