using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Entities.Consent
{
    public sealed class History : Entity
    {
        public History(string description) : base()
        {
            this.Description = description;
        }
        public string Description{ get; set; }
    }
}
