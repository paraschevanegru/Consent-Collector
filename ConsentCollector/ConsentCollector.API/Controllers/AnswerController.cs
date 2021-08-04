using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ConsentCollector.API.Controllers
{
    [Route("api/v1/answer")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        
        private readonly IAnswerService answerService;

        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {

            var answers = answerService.GetAll();

            return Ok(answers);
        }

        [HttpGet("user/{userId}/survey/{surveyId}")]
        public IActionResult GetUserAndSurveyId([FromRoute] Guid userId,[FromRoute] Guid surveyId)
        {
            var result = answerService.GetByUserAndSurveyId(userId,surveyId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await answerService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnswerModel model)
        {
            var result = await answerService.Create(model);
            return Created(result.Id.ToString(), result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await answerService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateAnswerModel model)
        {
            await answerService.Update(id, model);

            return NoContent();
        }
    }
}
