using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;

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
