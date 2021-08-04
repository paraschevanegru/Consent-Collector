using ConsentCollector.Entities.Consent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.HistoryRepository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ConsentContext context;
        public HistoryRepository(ConsentContext context)
        {
            this.context = context;
        }
        public async Task Create(History history)
        {
            await this.context.History.AddAsync(history);
        }

        public IEnumerable<History> GetAll()
        {
            return context.History;
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
