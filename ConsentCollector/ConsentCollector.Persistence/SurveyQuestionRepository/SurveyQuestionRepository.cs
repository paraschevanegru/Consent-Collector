using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.SurveyQuestionRepository
{
    public sealed class SurveyQuestionRepository : ISurveyQuestionRepository
    {
        private readonly ConsentContext context;
        public SurveyQuestionRepository(ConsentContext context)
        {
            this.context = context;
        }
        public async Task Create(SurveyQuestion surveyQuestion)
        {
            await this.context.SurveyQuestion.AddAsync(surveyQuestion);
        }

        public void Delete(SurveyQuestion surveyQuestion)
        {
            context.SurveyQuestion.Remove(surveyQuestion);
        }

        public void DeleteBySurveyId(Guid surveyId)
        {
            var idList = context.SurveyQuestion.Where(a => a.IdSurvey == surveyId).ToList();
            foreach (var survey in idList)
            {
                context.SurveyQuestion.Remove(survey);
            }
           
        }

        public IEnumerable<SurveyQuestion> GetAll()
        {
            return context.SurveyQuestion;
        }

        public async Task<SurveyQuestion> GetSurveyQuestionById(Guid id)
        {
            return await context.SurveyQuestion.FirstAsync(a => a.Id == id);
        }

        public IEnumerable<SurveyQuestion> GetSurveyQuestionBySurveyId(Guid surveyId)
        {
            return context.SurveyQuestion.Where(a => a.IdSurvey == surveyId).ToList();
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }

        public void Update(SurveyQuestion surveyQuestion)
        {
            this.context.Update(surveyQuestion);
        }
    }
}
