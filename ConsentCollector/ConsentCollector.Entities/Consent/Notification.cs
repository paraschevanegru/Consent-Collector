﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Entities.Consent
{
    public sealed class Notification : Entity
    {
        public Notification(string title, string description) : base()
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public Guid IdUser { get; set; }

        public Guid IdSurvey { get; set; }

        public User User { get; set; }

        public Survey Survey { get; set; }
    }
}
