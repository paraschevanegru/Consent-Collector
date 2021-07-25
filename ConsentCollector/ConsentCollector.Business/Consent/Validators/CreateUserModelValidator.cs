using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Models.Users;
using FluentValidation;

namespace ConsentCollector.Business.Consent.Validators
{
    public sealed class CreateUserModelValidator : AbstractValidator<CreateUserModel>
    {

        public CreateUserModelValidator()
        {
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3, 20)
                .Matches(@"^[a-zA-Z0-9]*$")
                .WithMessage("{PropertyName} should not contain special character(s)!");

            List<string> role = new List<string>() { "user", "admin" };


            RuleFor(x => x.Role)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Must(x => role.Contains(x))
                .WithMessage("Please only use: " + String.Join(",", role));

        }
    }
}
