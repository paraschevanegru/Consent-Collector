using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ConsentCollector.API.Controllers
{
    [Route("api/v1/consent")]
    [ApiController]
    public class ConsentCollector:ControllerBase
    {
        private readonly ISurveyService surveyService;
        public ConsentCollector(ISurveyService surveyService)
        {
            this.surveyService = surveyService;
        }

        [HttpGet]
        public IActionResult GetAll(string launchDateTime = "", string expirationDateTime ="", string? legalBasis = "")
        {
            try
            {
                var survey = surveyService.GetAll(launchDateTime != "" ? DateTime.Parse(launchDateTime) : null,
                    expirationDateTime != "" ? DateTime.Parse(expirationDateTime) : null, legalBasis);
                return Ok(survey);
            }
            // Least specific:
            catch (Exception)
            {
                return BadRequest();
            }
           

            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await surveyService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSurveyModel model)
        {
            var result = await surveyService.Create(model);
            return Created(result.Id.ToString(), result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await surveyService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateSurveyModel model)
        {
            await surveyService.Update(id, model);

            return NoContent();
        }
    }
}
