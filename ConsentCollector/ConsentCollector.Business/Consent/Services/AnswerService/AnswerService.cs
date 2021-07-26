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
    public sealed class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository answerRepository;
        private readonly IMapper mapper;
        public AnswerService(IAnswerRepository answerRepository,IMapper mapper)
        {
            this.mapper = mapper;
            this.answerRepository = answerRepository;
        }

        public IEnumerable<AnswerModel> GetByUserAndSurveyId(Guid userId, Guid surveyId)
        {
            var answer = answerRepository.GetAnswerByUserAndSurveyId(userId, surveyId);
            return mapper.Map<IEnumerable<AnswerModel>>(answer);
        }

        public async Task<AnswerModel> Create(CreateAnswerModel model)
        {
            var answer = this.mapper.Map<Answer>(model);
            await this.answerRepository.Create(answer);
            await this.answerRepository.SaveChanges();
            return mapper.Map<AnswerModel>(answer);
        }

        public async Task Delete(Guid answerId)
        {
            var answer = await answerRepository.GetAnswerById(answerId);

            answerRepository.Delete(answer);

            await answerRepository.SaveChanges();
        }

        public async Task<AnswerModel> GetById(Guid id)
        {
            var answer = await answerRepository.GetAnswerById(id);
            return mapper.Map<AnswerModel>(answer);
        }

        public async Task Update(Guid answerId, CreateAnswerModel model)
        {
            var answer = await answerRepository.GetAnswerById(answerId);

            mapper.Map(model, answer);

            answerRepository.Update(answer);

            await answerRepository.SaveChanges();
        }

        public IEnumerable<AnswerModel> GetAll()
        {
            var answer = answerRepository.GetAll();

            return mapper.Map<IEnumerable<AnswerModel>>(answer);
        }
    }
}
