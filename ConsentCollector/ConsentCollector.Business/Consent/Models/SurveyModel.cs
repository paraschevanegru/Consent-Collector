using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Models
{
    public sealed class SurveyModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public byte[] Description { get; set; }
        public string LegalBasis { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
