using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Models.UserDetails;
using FluentValidation;

namespace ConsentCollector.Business.Consent.Validators
{
    public sealed class CreateUserDetailModelValidator : AbstractValidator<CreateUserDetailModel>
    {
        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }

        public CreateUserDetailModelValidator()
        {
            RuleFor(x => x.Firstname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3,30)
                .Must(IsValidName).WithMessage("{PropertyName} should be all letters.");

            RuleFor(x => x.Lastname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3, 30)
                .Must(IsValidName).WithMessage("{PropertyName} should be all letters.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .EmailAddress();

            RuleFor(x => x.Number)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(10)
                .Matches(@"^[\d()]*\d[\d()]*\d+$");

        }
    }
}
