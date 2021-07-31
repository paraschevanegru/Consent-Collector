using ConsentCollector.Business.Consent.Models.SurveyQuestionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.SurveyQuestionService
{
    public interface ISurveyQuestionService
    {
        IEnumerable<SurveyQuestionModel> GetAll();
        Task<SurveyQuestionModel> GetById(Guid id);

        IEnumerable<SurveyQuestionModel> GetBySurveyId(Guid surveyId);

        Task<SurveyQuestionModel> Create(CreateSurveyQuestionModel model);

        Task Delete(Guid surveyQuestionId);

        Task DeleteBySurvey(Guid surveyId);

        Task Update(Guid surveyQuestionId, CreateSurveyQuestionModel model);
    }
}
