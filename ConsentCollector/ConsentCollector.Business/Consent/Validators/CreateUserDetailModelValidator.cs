using ConsentCollector.Business.Consent.Models.UserDetails;
using FluentValidation;

namespace ConsentCollector.Business.Consent.Validators
{
    public sealed class CreateUserDetailModelValidator : AbstractValidator<CreateUserDetailModel>
    {


        public CreateUserDetailModelValidator()
        {
            RuleFor(x => x.Firstname)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3,30)
                .Matches(@"^[A-Z][-a-zA-Z]+$")
                .WithMessage("{PropertyName} should have a specific format!");

            RuleFor(x => x.Lastname)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3, 30)
                .Matches(@"^[A-Z][-a-zA-Z]+$")
                .WithMessage("{PropertyName} should have a specific format!");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .EmailAddress();

            RuleFor(x => x.Number)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(10)
                .Matches(@"^[\d()]*\d[\d()]*\d+$");

        }
    }
}
