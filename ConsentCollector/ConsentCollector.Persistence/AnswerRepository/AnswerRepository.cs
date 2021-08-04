using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ConsentContext context;
        public AnswerRepository(ConsentContext context)
        {
            this.context = context;
        }

        public IEnumerable<Answer> GetAnswerByUserAndSurveyId(Guid userId, Guid surveyId)
        {
            return context.Answer.Where(a => a.IdUser == userId && a.IdSurvey == surveyId).ToList();
        }

        public async Task Create(Answer answer)
        {
            await this.context.Answer.AddAsync(answer);
        }

        public void Delete(Answer answer)
        {
            context.Answer.Remove(answer);
        }

        public IEnumerable<Answer> GetAll()
        { 
            return context.Answer;
        }

        public async Task<Answer> GetAnswerById(Guid id)
        {
            return await context.Answer
                .FirstAsync(a => a.Id == id);
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }

        public void Update(Answer answer)
        {
            this.context.Answer.Update(answer);
        }
    }
}
