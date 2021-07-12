using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public interface IConsentRepository
    {
        Task<Survey> GetSurveyById(Guid id);

        Task Create(Survey survey);

        Task SaveChanges();

        void Update(Survey survey);
    }
}
