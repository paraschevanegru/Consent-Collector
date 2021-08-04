using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Models
{
    public sealed class CreateNotificationModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Seen { get; set; }
        public Guid IdSurvey { get; set; }
        public Guid IdUser { get; set; }
    }
}
