using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Entities.Consent
{
    public sealed class Questions : Entity
    {
        public Questions(bool optional, string question):base()
        {
            Optional = optional;
            Question = question;
        }
        public bool Optional { get; set; }
        public string Question { get; set; }
    }
}
