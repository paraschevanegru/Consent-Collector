using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Entities.Consent
{
    public sealed class UserDetail : Entity
    {

        //public UserDetail()
        //{

        //}
        public UserDetail(Guid idUser, string firstname, string lastname, string number, string email) : base()
        {
            IdUser = idUser;
            Firstname = firstname;
            Lastname = lastname;
            Number = number;
            Email = email;
        }

        public Guid IdUser { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public User User { get; set; }
    }
}
