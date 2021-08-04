using ConsentCollector.Business.Consent.Models.SurveyQuestionModel;
using ConsentCollector.Business.Consent.Services.SurveyQuestionService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ConsentCollector.API.Controllers
{
    [Route("api/v1/surveyQuestion")]
    [ApiController]
    public class SurveyQuestionController : Controller
    {
        private readonly ISurveyQuestionService surveyQuestionService;
        public SurveyQuestionController(ISurveyQuestionService surveyQuestionService)
        {
            this.surveyQuestionService = surveyQuestionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var answers = surveyQuestionService.GetAll();

            return Ok(answers);
        }

        [HttpGet("survey/{surveyId}")]
        public IActionResult GetUserAndSurveyId([FromRoute] Guid surveyId)
        {
            var result = surveyQuestionService.GetBySurveyId(surveyId);
            return Ok(result);
        }

        [HttpDelete("survey/{surveyId}")]
        public async Task<IActionResult> DeleteBySurveyId([FromRoute] Guid surveyId)
        {
            await surveyQuestionService.DeleteBySurvey(surveyId);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await surveyQuestionService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSurveyQuestionModel model)
        {
            var result = await surveyQuestionService.Create(model);
            return Created(result.Id.ToString(), result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await surveyQuestionService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateSurveyQuestionModel model)
        {
            await surveyQuestionService.Update(id, model);

            return NoContent();
        }
    }
}
