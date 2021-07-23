using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Entities.Consent
{
    public sealed class Survey:Entity
    {
        public Survey(string subject,string description,string legalBasis,DateTime launchDate,DateTime expirationDate)
        {
            this.Subject = subject;
            this.Description = description;
            this.LegalBasis = legalBasis;
            this.LaunchDate = launchDate;
            this.ExpirationDate = expirationDate;
        }

        public string Subject { get; set; }
        public string Description { get; set; }
        public string LegalBasis { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<SurveyQuestion> SurveyQuestion { get; set; } = new List<SurveyQuestion>();

        //public Guid IdQuestion { get; set; }
    }
}
