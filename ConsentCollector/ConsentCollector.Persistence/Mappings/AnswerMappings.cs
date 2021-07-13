using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.Mappings
{
    internal abstract class AnswerMappings
    {
        internal static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(e =>
            {
                e.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Agree)
                .HasColumnName("Agree")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.AnswerDate)
                .HasColumnName("AnswerDate")
                .IsRequired()
                .ValueGeneratedNever();
            });
        }
    }
}
