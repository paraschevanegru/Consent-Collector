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

        //public Survey()
        //{

        //}
        //public Survey(Guid idQuestion, string subject, byte[] description, string legalBasis, DateTime launchDate, DateTime expirationDate):base()
        //{
        //    Subject = subject;
        //    Description = description;
        //    LegalBasis = legalBasis;
        //    LaunchDate = launchDate;
        //    ExpirationDate = expirationDate;
        //    IdQuestion = idQuestion;
        //}

        public string Subject { get; set; }
        public byte[] Description { get; set; }
        public string LegalBasis { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        //public Guid IdQuestion { get; set; }
    }
}
