using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.QuestionRepository
{
    public sealed class QuestionRepository : IQuestionRepository
    {
        private readonly ConsentContext context;
        public QuestionRepository(ConsentContext context)
        {
            this.context = context;
        }
        public async Task Create(Question question)
        {
            await this.context.Question.AddAsync(question);
        }

        public void Delete(Question question)
        {
            context.Question.Remove(question);
        }

        public IEnumerable<Question> GetAll()
        {
            return context.Question;
        }

        public async Task<Question> GetQuestionById(Guid id)
        {
            return await context.Question.FirstAsync(q => q.Id == id);
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }

        public void Update(Question question)
        {
            this.context.Question.Update(question);
        }
    }
}
