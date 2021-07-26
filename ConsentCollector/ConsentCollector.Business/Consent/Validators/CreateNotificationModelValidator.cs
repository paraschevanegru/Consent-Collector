using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Models;
using FluentValidation;

namespace ConsentCollector.Business.Consent.Validators
{
    public sealed class CreateNotificationModelValidator : AbstractValidator<NotificationModel>
    {
        public CreateNotificationModelValidator()
        {
            RuleFor(x => x.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3, 50);

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3, 150);
        }
    }
}
