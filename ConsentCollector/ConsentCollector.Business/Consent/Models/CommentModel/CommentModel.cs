using System;

namespace ConsentCollector.Business.Consent.Models.CommentModel
{
    public sealed class CommentModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdSurvey { get; set; }
        public string Text { get; set; }
    }
}
