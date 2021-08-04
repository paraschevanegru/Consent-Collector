using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.SurveyQuestionRepository
{
    public interface ISurveyQuestionRepository
    {
        IEnumerable<SurveyQuestion> GetAll();
        public Task<SurveyQuestion> GetSurveyQuestionById(Guid id);
        public IEnumerable<SurveyQuestion> GetSurveyQuestionBySurveyId(Guid surveyId);
        Task Create(SurveyQuestion surveyQuestion);

        Task SaveChanges();

        void Delete(SurveyQuestion surveyQuestion);
        void DeleteBySurveyId(Guid surveyId);
        void Update(SurveyQuestion surveyQuestion);
    }
}
