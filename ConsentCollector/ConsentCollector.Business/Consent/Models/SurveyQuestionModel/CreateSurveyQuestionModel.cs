using System;

namespace ConsentCollector.Business.Consent.Models.SurveyQuestionModel
{
    public sealed class CreateSurveyQuestionModel
    {
        public Guid IdSurvey { get; set; }

        public Guid IdQuestion { get; set; }
    }
}
