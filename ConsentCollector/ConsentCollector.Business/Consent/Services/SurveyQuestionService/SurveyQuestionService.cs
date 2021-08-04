using AutoMapper;
using ConsentCollector.Business.Consent.Models.SurveyQuestionModel;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.SurveyQuestionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.SurveyQuestionService
{
    public sealed class SurveyQuestionService : ISurveyQuestionService
    {
        private readonly ISurveyQuestionRepository surveyQuestionRepository;
        private readonly IMapper mapper;
        public SurveyQuestionService(ISurveyQuestionRepository surveyQuestionRepository, IMapper mapper)
        {
            this.surveyQuestionRepository = surveyQuestionRepository;
            this.mapper = mapper;
        }
        public async Task<SurveyQuestionModel> Create(CreateSurveyQuestionModel model)
        {
            var surveyQuestion = this.mapper.Map<SurveyQuestion>(model);
            await this.surveyQuestionRepository.Create(surveyQuestion);
            await this.surveyQuestionRepository.SaveChanges();
            return mapper.Map<SurveyQuestionModel>(surveyQuestion);
        }

        public async Task Delete(Guid surveyQuestionId)
        {
            var surveyQuestion = await surveyQuestionRepository.GetSurveyQuestionById(surveyQuestionId);
            surveyQuestionRepository.Delete(surveyQuestion);
            await surveyQuestionRepository.SaveChanges();
        }

        public async Task DeleteBySurvey(Guid surveyId)
        {
            surveyQuestionRepository.DeleteBySurveyId(surveyId);
            await surveyQuestionRepository.SaveChanges();
        }

        public IEnumerable<SurveyQuestionModel> GetAll()
        {
            var surveyQuestion = surveyQuestionRepository.GetAll();
            return mapper.Map<IEnumerable<SurveyQuestionModel>>(surveyQuestion);
        }

        public async Task<SurveyQuestionModel> GetById(Guid id)
        {
            var surveyQuestion = await surveyQuestionRepository.GetSurveyQuestionById(id);
            return mapper.Map<SurveyQuestionModel>(surveyQuestion);
        }

        public IEnumerable<SurveyQuestionModel> GetBySurveyId(Guid surveyId)
        {
            var surveyQuestion = surveyQuestionRepository.GetSurveyQuestionBySurveyId(surveyId);
            return mapper.Map<IEnumerable<SurveyQuestionModel>>(surveyQuestion);
        }

        public async Task Update(Guid surveyQuestionId, CreateSurveyQuestionModel model)
        {
            var surveyQuestion = await surveyQuestionRepository.GetSurveyQuestionById(surveyQuestionId);
            mapper.Map(model, surveyQuestion);
            surveyQuestionRepository.Update(surveyQuestion);
            await surveyQuestionRepository.SaveChanges();
        }
    }
}
