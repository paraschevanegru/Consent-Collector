using System;

namespace ConsentCollector.Entities.Consent
{
    public sealed class Answer : Entity
    {
        public Answer(bool agree, DateTime answerDate) : base()
        {
            Agree = agree;
            AnswerDate = answerDate;
        }

        public bool Agree { get; set; }
        public DateTime AnswerDate { get; set; }

        public Guid IdUser { get; set; }

        public Guid IdSurvey { get; set; }

        public Guid IdQuestion { get; set; }

        public User User { get; set; }

        public Survey Survey { get; set; }

        public Question Question { get; set; }
    }
}
