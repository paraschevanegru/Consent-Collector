﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Entities.Consent
{
    public sealed class Comment:Entity
    {
        public Comment(Guid idUser, Guid idSurvey, string text) : base()
        {
            Text = text;
            IdUser = idUser;
            IdSurvey = idSurvey;
        }

        public string Text { get; set; }

        public Guid IdUser { get; set; }

        public Guid IdSurvey { get; set; }

        public User User { get; set; }

        public Survey Survey { get; set; }
    }
}
