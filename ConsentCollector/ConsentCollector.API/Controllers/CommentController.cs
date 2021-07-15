﻿using ConsentCollector.Business.Consent.Models.CommentModel;
using ConsentCollector.Business.Consent.Services.CommentService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsentCollector.API.Controllers
{
    [Route("api/v1/comment")]
    [ApiController]
    public class CommentCollector : ControllerBase
    {
        private readonly ICommentService commentService;
        public CommentCollector(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await commentService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentModel model)
        {
            var result = await commentService.Create(model);
            return Created(result.Id.ToString(), null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await commentService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateCommentModel model)
        {
            await commentService.Update(id, model);

            return NoContent();
        }
    }
}
