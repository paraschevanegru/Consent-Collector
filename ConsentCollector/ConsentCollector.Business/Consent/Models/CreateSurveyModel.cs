using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Models
{
    public sealed class CreateSurveyModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string LegalBasis { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
