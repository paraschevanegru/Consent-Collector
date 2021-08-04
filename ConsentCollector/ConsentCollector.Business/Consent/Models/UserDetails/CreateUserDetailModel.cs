using System;

namespace ConsentCollector.Business.Consent.Models.UserDetails
{
    public sealed class CreateUserDetailModel
    {
        public Guid IdUser { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
    }
}
