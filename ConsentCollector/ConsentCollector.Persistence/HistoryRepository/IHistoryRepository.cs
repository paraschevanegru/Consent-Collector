using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
