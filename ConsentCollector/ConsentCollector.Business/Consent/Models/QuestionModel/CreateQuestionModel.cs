﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Models.QuestionModel
{
    public sealed class CreateQuestionModel
    {
        public bool Optional { get; set; }
        public string Questions { get; set; }
    }
}
