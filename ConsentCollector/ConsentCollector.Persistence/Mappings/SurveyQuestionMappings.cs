using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.Mappings
{
    internal abstract class SurveyQuestionMappings
    {
        internal static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SurveyQuestion>(e =>
            {
                e.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.IdSurvey)
                .HasColumnName("IdSurvey")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.IdQuestion)
                .HasColumnName("IdQuestion")
                .IsRequired()
                .ValueGeneratedNever();
            });
        }
    }
}
