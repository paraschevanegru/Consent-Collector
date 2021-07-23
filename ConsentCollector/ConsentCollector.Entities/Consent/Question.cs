using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Entities.Consent
{
    public sealed class Question : Entity
    {
        public Question(bool optional, string questions) : base()
        {
            Optional = optional;
            Questions = questions;
        }
        public bool Optional { get; set; }
        public string Questions { get; set; }

        public ICollection<Answer>  Answers { get; set; } = new List<Answer>();
        public ICollection<SurveyQuestion> SurveyQuestion { get; set; } = new List<SurveyQuestion>();
    }
}
