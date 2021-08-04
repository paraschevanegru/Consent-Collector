using ConsentCollector.Business.Consent.Models;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace ConsentCollector.Business.Consent.Validators
{
    public sealed class CreateSurveyModelValidator :AbstractValidator<CreateSurveyModel>
    {
        public CreateSurveyModelValidator()
        {
            List<string> legalBasis = new List<string>() { "Contract", "Law", "Legitimate Interest" };

            RuleFor(x => x.LegalBasis)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Must(x => legalBasis.Contains(x))
                .WithMessage("Please only use: " + String.Join(",", legalBasis));

            RuleFor(x => x.Subject)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3, 50);

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3, 150);

            RuleFor(x => x.LaunchDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .GreaterThanOrEqualTo(x => DateTime.Now.Date)
                .WithMessage("{PropertyName} must be equal or greater than current date!");

            RuleFor(x => x.ExpirationDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .GreaterThan(x => x.LaunchDate)
                .WithMessage("{PropertyName} must be equal or greater than current date!");
        }
    }
}
