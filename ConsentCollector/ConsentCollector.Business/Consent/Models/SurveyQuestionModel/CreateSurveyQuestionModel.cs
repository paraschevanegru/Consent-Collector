using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Models.SurveyQuestionModel
{
    public sealed class CreateSurveyQuestionModel
    {
        public Guid IdSurvey { get; set; }

        public Guid IdQuestion { get; set; }
    }
}
