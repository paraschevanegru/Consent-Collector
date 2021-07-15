using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.CommentRepository
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentById(Guid id);
        Task Create(Comment comment);
        Task SaveChanges();
        void Delete(Comment comment);
        void Update(Comment comment);
    }
}
