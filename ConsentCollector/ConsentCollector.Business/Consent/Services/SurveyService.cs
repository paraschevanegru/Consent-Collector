using AutoMapper;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Entities.Consent;
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
        private readonly IMapper mapper;
        public SurveyService(IConsentRepository consentRepository, IMapper mapper)
        {
            this.consentRepository = consentRepository;
            this.mapper = mapper;
        }
        public async Task<SurveyModel> GetById(Guid id)
        {
            var survey = await consentRepository.GetSurveyById(id);
            //return new SurveyModel
            //{
            //    Id = survey.Id,
            //    Subject = survey.Subject,
            //    Description = survey.Description,
            //    LegalBasis = survey.LegalBasis,
            //    LaunchDate = survey.LaunchDate,
            //    ExpirationDate = survey.ExpirationDate
            //};
             return mapper.Map<SurveyModel>(survey);
        }

        public async Task<SurveyModel> Create(CreateSurveyModel model)
        {
            var survey = this.mapper.Map<Survey>(model);

            await this.consentRepository.Create(survey);

            await this.consentRepository.SaveChanges();

            return mapper.Map<SurveyModel>(survey);
        }

        public async Task Delete(Guid surveyId)
        {
            var survey = await consentRepository.GetSurveyById(surveyId);

            consentRepository.Delete(survey);

            await consentRepository.SaveChanges();
        }

        public async Task Update(Guid surveyId, CreateSurveyModel model)
        {
            var survey = await consentRepository.GetSurveyById(surveyId);

            mapper.Map(model, survey);

            consentRepository.Update(survey);

            await consentRepository.SaveChanges();
        }

        public IEnumerable<SurveyModel> GetAll(DateTime? launchDateTime = null, DateTime? expirationDateTime = null, string? legalBasis = "")
        {
            var survey = consentRepository.GetAll(launchDateTime, expirationDateTime, legalBasis);

            return mapper.Map<IEnumerable<SurveyModel>>(survey);
        }
    }
}
