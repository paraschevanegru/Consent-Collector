using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Entities.Consent
{
    public sealed class User:Entity
    {
        //public Users(string username,string password,string role) : base()
        //{
        //    Username = username;
        //    Password = password;
        //    Role = role;
        //}

        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public UserDetail Detail { get; set; }

        //public void Ceva()
        //{
        //    new Users
        //    {
        //        Username = "username",
        //        Password = "password"
        //    };
        //}
    }
}
