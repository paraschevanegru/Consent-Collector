using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetAll();
        Task<Answer> GetAnswerById(Guid id);

        IEnumerable<Answer> GetAnswerByUserAndSurveyId(Guid userId, Guid surveyId);
        Task Create(Answer answer);

        Task SaveChanges();

        void Delete(Answer answer);
        void Update(Answer answer);
    }
}
