﻿using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.Mappings
{
    internal abstract class SurveyMappings
    {
        internal static void Map(ModelBuilder modelBuilder)
        {
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

                e.HasMany(s => s.Comments)
                .WithOne(c => c.Survey)
                .HasForeignKey(s => s.IdSurvey);

                e.HasMany(s => s.Answers)
              .WithOne(a => a.Survey)
              .HasForeignKey(s => s.IdSurvey);

                e.HasMany(s => s.Notifications)
                 .WithOne(n => n.Survey)
                 .HasForeignKey(s => s.IdSurvey);
            });
        }
    }
}