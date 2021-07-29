using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Models.Users;
using ConsentCollector.Business.Consent.Services.Users;

namespace ConsentCollector.API.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpOptions]
        public IActionResult Options()
        {
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAll()
        {

            var users = userService.GetAll();

            return Ok(users);
        }

        [HttpGet("username/{username}/password/{password}")]
        public async Task<IActionResult> GetUsernameAndPassword([FromRoute] string username, [FromRoute] string password)
        {
            var result = await userService.GetByUsername(username);
            bool verified = BCrypt.Net.BCrypt.Verify(password, result.Password);

            if (verified)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(403);
            }
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await userService.GetById(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserModel model)
        {
            var result = await userService.Create(model);
            return Created(result.Id.ToString(), result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await userService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateUserModel model)
        {
            await userService.Update(id, model);

            return NoContent();
        }

    }
}
