using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public interface IConsentRepository
    {
        Task<Survey> GetSurveyById(Guid id);

        IEnumerable<Survey> GetAll(DateTime? launchDateTime = null, DateTime? expirationDateTime = null, string? legalBasis = "");

        Task Create(Survey survey);

        Task SaveChanges();

        void Delete(Survey survey);
        void Update(Survey survey);
    }
}
