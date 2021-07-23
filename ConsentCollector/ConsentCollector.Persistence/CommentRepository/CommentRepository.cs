using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.CommentRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ConsentContext context;

        public CommentRepository(ConsentContext context)
        {
            this.context = context;
        }
        public async Task Create(Comment comment)
        {
            await this.context.Comment.AddAsync(comment);
        }

        public void Delete(Comment comment)
        {
            context.Comment.Remove(comment);
        }

        public IEnumerable<Comment> GetAll()
        {
            return context.Comment;
        }

        public async Task<Comment> GetCommentById(Guid id)
        {
            return await context.Comment
                .FirstAsync(s => s.Id == id);
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }

        public void Update(Comment comment)
        {
            this.context.Comment.Update(comment);
        }
    }
}
