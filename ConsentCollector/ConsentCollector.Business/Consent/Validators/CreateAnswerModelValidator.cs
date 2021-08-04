using ConsentCollector.Business.Consent.Models;
using FluentValidation;
using System;

namespace ConsentCollector.Business.Consent.Validators
{
    public sealed class CreateAnswerModelValidator : AbstractValidator<CreateAnswerModel>
    {
        public CreateAnswerModelValidator()
        {
            RuleFor(x => x.AnswerDate)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("{PropertyName} should be not empty.")
                .GreaterThanOrEqualTo(x => DateTime.Now.Date)
                .WithMessage("{PropertyName} must be equal or greater than current date!");
        }
    }
}
