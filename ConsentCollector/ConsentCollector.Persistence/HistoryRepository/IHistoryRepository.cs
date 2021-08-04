using ConsentCollector.Entities.Consent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.HistoryRepository
{
    public interface IHistoryRepository
    {
        IEnumerable<History> GetAll();
        Task Create(History history);
        Task SaveChanges();
    }
}
