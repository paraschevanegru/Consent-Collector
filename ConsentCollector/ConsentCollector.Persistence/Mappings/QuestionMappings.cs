using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                e.HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(q => q.IdQuestion);
            });
        }
    }
}
