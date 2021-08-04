using ConsentCollector.Business.Consent.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services
{
    public interface ISurveyService
    {
        Task<SurveyModel> GetById(Guid id);

        Task<SurveyModel> Create(CreateSurveyModel model);

        Task Delete(Guid surveyId);

        Task Update(Guid surveyId, CreateSurveyModel model);

        IEnumerable<SurveyModel> GetAll(DateTime? launchDateTime = null, DateTime? expirationDateTime =null, string? legalBasis = "");
    }
}
