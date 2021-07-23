using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Models.UserDetails;
using ConsentCollector.Business.Consent.Services.UserDetails;

namespace ConsentCollector.API.Controllers
{
    [Route("api/v1/detail")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly IUserDetailService userDetailService;

        public UserDetailController(IUserDetailService userDetailService)
        {
            this.userDetailService = userDetailService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var details = userDetailService.GetAll();

            return Ok(details);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await userDetailService.GetById(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDetailModel model)
        {
            var result = await userDetailService.Create(model);
            return Created(result.Id.ToString(), result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await userDetailService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateUserDetailModel model)
        {
            await userDetailService.Update(id, model);

            return NoContent();
        }
    }
}
