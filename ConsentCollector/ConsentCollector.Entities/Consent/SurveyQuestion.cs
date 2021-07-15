using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Entities.Consent
{
    public sealed class SurveyQuestion : Entity
    {
        public Guid IdSurvey { get; set; }
        public Guid IdQuestion { get; set; }
        public Question Question { get; set; }
        public Survey Survey { get; set; }
    }
}
