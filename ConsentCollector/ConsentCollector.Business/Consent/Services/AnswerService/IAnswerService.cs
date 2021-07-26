using ConsentCollector.Business.Consent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services
{
    public interface IAnswerService
    {
        IEnumerable<AnswerModel> GetAll();
        Task<AnswerModel> GetById(Guid id);

        IEnumerable<AnswerModel> GetByUserAndSurveyId(Guid userId, Guid surveyId);

        Task<AnswerModel> Create(CreateAnswerModel model);

        Task Delete(Guid answerId);

        Task Update(Guid answerId, CreateAnswerModel model);

    }
}
