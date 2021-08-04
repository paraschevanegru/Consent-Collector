using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;

namespace ConsentCollector.Persistence.Mappings
{
    internal abstract class CommentMappings
    {
        internal static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(e =>
            {
                e.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedNever();

                e.Property(c => c.Text)
                .HasColumnName("Text")
                .IsRequired()
                .ValueGeneratedNever();

                e.HasCheckConstraint("CK_Comment_Text", "Len([Text])>=5 and Len([Text])<=100");
            });
        }
    }
}
