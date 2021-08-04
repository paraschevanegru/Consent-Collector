using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsentCollector.API.Controllers
{
    [Route("api/v1/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;
        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var notification = notificationService.GetAll();

            return Ok(notification);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await notificationService.GetById(id);
            return Ok(result);
        }
        [HttpGet("user/{userId}")]
        public  IActionResult GetUserId([FromRoute] Guid userId)
        {
            var result = notificationService.GetByUserId(userId);
            return Ok(result);
        }
        [HttpGet("user/{userId}/seen/{seen}")]
        public IActionResult GetUserIdAndSeen([FromRoute] Guid userId, [FromRoute] bool seen)
        {
            var result = notificationService.GetByUserIdAndSeen(userId, seen);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateNotificationModel model)
        {
            var result = await notificationService.Create(model);
            return Created(result.IdSurvey.ToString(),result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] NotificationModel model)
        {
            await notificationService.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await notificationService.Delete(id);

            return NoContent();
        }
    }
}
