using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Models
{
    public sealed class AnswerModel
    {
        public Guid Id { get; set; }
        public bool Agree { get; set; }
        public DateTime AnswerDate { get; set; }

        public Guid IdUser { get; set; }

        public Guid IdSurvey { get; set; }

        public Guid IdQuestion { get; set; }
    }
}
