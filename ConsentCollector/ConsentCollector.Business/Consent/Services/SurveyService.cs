using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services
{
    public sealed class SurveyService : ISurveyService
    {
        private readonly IConsentRepository consentRepository;
        //private readonly IMapper mapper;
        public SurveyService(IConsentRepository consentRepository)
        {
            this.consentRepository = consentRepository;
        }
        public async Task<SurveyModel> GetById(Guid id)
        {
            var survey = await consentRepository.GetSurveyById(id);
            return new SurveyModel
            {
                Id = survey.Id,
                Subject = survey.Subject,
                Description = survey.Description,
                LegalBasis = survey.LegalBasis,
                LaunchDate = survey.LaunchDate,
                ExpirationDate = survey.ExpirationDate
            };
        }

        public async Task<SurveyModel> Create(CreateSurveyModel model)
        {

        }
    }
}
