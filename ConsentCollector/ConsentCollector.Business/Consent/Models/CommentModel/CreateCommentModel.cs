using System;

namespace ConsentCollector.Business.Consent.Models.CommentModel
{
    public sealed class CreateCommentModel
    {
        public string Text { get; set; }

        public Guid IdUser { get; set; }

        public Guid IdSurvey { get; set; }
    }
}
