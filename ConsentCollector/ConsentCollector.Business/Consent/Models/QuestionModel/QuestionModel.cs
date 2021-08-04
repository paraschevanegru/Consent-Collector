using System;

namespace ConsentCollector.Business.Consent.Models.QuestionModel
{
    public sealed class QuestionModel
    {
        public Guid Id { get; set; }
        public bool Optional { get; set; }
        public string Questions { get; set; }
    }
}
