using System;

namespace ConsentCollector.Business.Consent.Models.SurveyQuestionModel
{
    public sealed class SurveyQuestionModel
    {
        public Guid Id { get; set; }
        public Guid IdSurvey { get; set; }
        public Guid IdQuestion { get; set; }
    }
}
