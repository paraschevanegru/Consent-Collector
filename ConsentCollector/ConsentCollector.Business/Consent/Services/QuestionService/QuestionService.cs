using AutoMapper;
using ConsentCollector.Business.Consent.Models.QuestionModel;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.QuestionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.QuestionService
{
    public sealed class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IMapper mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }
        public async Task<QuestionModel> Create(CreateQuestionModel model)
        {
            var question = this.mapper.Map<Question>(model);

            await this.questionRepository.Create(question);

            await this.questionRepository.SaveChanges();

            return mapper.Map<QuestionModel>(question);
        }

        public async Task Delete(Guid questionId)
        {
            var question = await questionRepository.GetQuestionById(questionId);

            questionRepository.Delete(question);

            await questionRepository.SaveChanges();
        }

        public IEnumerable<QuestionModel> GetAll()
        {
            var question = questionRepository.GetAll();

            return mapper.Map<IEnumerable<QuestionModel>>(question);
        }

        public async Task<QuestionModel> GetById(Guid id)
        {
            var question = await questionRepository.GetQuestionById(id);

            return mapper.Map<QuestionModel>(question);
        }

        public async Task Update(Guid questionId, CreateQuestionModel model)
        {
            var question = await questionRepository.GetQuestionById(questionId);

            mapper.Map(model, question);

            questionRepository.Update(question);

            await questionRepository.SaveChanges();
        }
    }
}
