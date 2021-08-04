using ConsentCollector.Business.Consent.Models.QuestionModel;
using ConsentCollector.Business.Consent.Services.QuestionService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsentCollector.API.Controllers
{
    [Route("api/v1/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {

            var answers = questionService.GetAll();

            return Ok(answers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await questionService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuestionModel model)
        {
            var result = await questionService.Create(model);
            return Created(result.Id.ToString(), result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await questionService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateQuestionModel model)
        {
            await questionService.Update(id, model);

            return NoContent();
        }
    }
}
