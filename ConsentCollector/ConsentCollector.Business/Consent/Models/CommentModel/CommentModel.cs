using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Models.CommentModel
{
    public sealed class CommentModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdSurvey { get; set; }
        public string Text { get; set; }
    }
}
