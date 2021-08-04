using AutoMapper;
using ConsentCollector.Business.Consent.Models.HistoryModel;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.HistoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.HistoryService
{
    public sealed class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository historyRepository;
        private readonly IMapper mapper;

        public HistoryService(IHistoryRepository historyRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.historyRepository = historyRepository;
        }
        public async Task<HistoryModel> Create(CreateHistoryModel model)
        {
            var history = this.mapper.Map<History>(model);

            await this.historyRepository.Create(history);

            await this.historyRepository.SaveChanges();

            return mapper.Map<HistoryModel>(history);
        }

        public IEnumerable<HistoryModel> GetAll()
        {
            var history = historyRepository.GetAll();
            return mapper.Map<IEnumerable<HistoryModel>>(history);
        }
    }
}
