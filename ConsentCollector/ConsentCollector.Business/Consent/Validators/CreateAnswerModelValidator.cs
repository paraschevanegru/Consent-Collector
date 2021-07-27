using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Models;
using FluentValidation;

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
