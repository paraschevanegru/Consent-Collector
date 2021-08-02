using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public class ConsentRepository : IConsentRepository
    {
        private readonly ConsentContext context;

        public ConsentRepository(ConsentContext context)
        {
            this.context = context;
        }
        public async Task Create(Survey survey)
        {
            await this.context.Survey.AddAsync(survey);
        }

        public void Delete(Survey survey)
        {
            context.Survey.Remove(survey);
        }

        public async Task<Survey> GetSurveyById(Guid id)
        {
            return await context.Survey
                .FirstAsync(s=> s.Id==id);
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync(); 
        }

        public void Update(Survey survey)
        {
            this.context.Survey.Update(survey);
        }

        public IEnumerable<Survey> GetAll(DateTime? launchDateTime = null, DateTime? expirationDateTime = null)
        {
            if (launchDateTime!=null&& expirationDateTime != null)
            {
                return context.Survey.Where(x => x.LaunchDate >= launchDateTime
                                                 && x.ExpirationDate <= expirationDateTime)
                    .OrderByDescending(x => x.LaunchDate);
            }else if (launchDateTime != null && expirationDateTime == null)
            {
                return context.Survey.Where(x => x.LaunchDate >= launchDateTime)
                    .OrderByDescending(x => x.LaunchDate);
            }else if (launchDateTime == null && expirationDateTime != null)
            {
                return context.Survey.Where(x => x.ExpirationDate <= expirationDateTime)
                    .OrderByDescending(x => x.ExpirationDate);
            }            
           return context.Survey;
        }
    }
}
