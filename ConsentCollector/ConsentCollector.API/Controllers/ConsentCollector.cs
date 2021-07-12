﻿using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Services;
using ConsentCollector.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsentCollector.API.Controllers
{
    public class ConsentCollector:ControllerBase
    {
        private readonly ISurveyService surveyService;
        public ConsentCollector(ISurveyService surveyService)
        {
            this.surveyService = surveyService;
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
            return Created(result.Id.ToString(), null);
        }
    }
}
