using ConsentCollector.Business.Consent.Models.HistoryModel;
using ConsentCollector.Business.Consent.Services.HistoryService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConsentCollector.API.Controllers
{
    [Route("api/v1/history")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService historyService;
        public HistoryController(IHistoryService historyService)
        {
            this.historyService = historyService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {

            var history = historyService.GetAll();

            return Ok(history);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHistoryModel model)
        {
            var result = await historyService.Create(model);
            return Created(result.Id.ToString(), result);
        }
    }
}
