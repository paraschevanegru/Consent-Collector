using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Models
{
    public sealed class NotificationModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Seen { get; set; }
        public Guid IdSurvey { get; set; }
        public Guid IdUser { get; set; }

        //public User User { get; set; }

        //public Survey Survey { get; set; }
    }
}
