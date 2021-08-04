using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;

namespace ConsentCollector.Persistence.Mappings
{
    internal abstract class QuestionMappings
    {
        internal static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(e =>
            {
                e.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Optional)
                .HasColumnName("Optional")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Questions)
                .HasColumnName("Questions")
                .IsRequired()
                .ValueGeneratedNever();

                e.HasCheckConstraint("CK_Question_Questions", "Len([Questions])>=5 and Len([Questions])<=100");

                e.HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(q => q.IdQuestion);

                e.HasMany(q => q.SurveyQuestion)
                .WithOne(sq => sq.Question)
                .HasForeignKey(q => q.IdQuestion);
            });
        }
    }
}
