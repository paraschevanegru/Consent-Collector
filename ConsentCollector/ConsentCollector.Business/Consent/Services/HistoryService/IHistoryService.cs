using ConsentCollector.Business.Consent.Models.HistoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.HistoryService
{
    public interface IHistoryService
    {
        IEnumerable<HistoryModel> GetAll();
        Task<HistoryModel> Create(CreateHistoryModel model);
    }
}
