using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.CommentRepository
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAll();
        Task<Comment> GetCommentById(Guid id);
        Task Create(Comment comment);
        Task SaveChanges();
        Task<Comment> GetAnswerByUserAndSurveyId(Guid userId, Guid surveyId);
        void Delete(Comment comment);
        void Update(Comment comment);
    }
}
