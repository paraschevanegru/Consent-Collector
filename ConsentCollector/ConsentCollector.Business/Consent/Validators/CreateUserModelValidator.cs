using ConsentCollector.Business.Consent.Models.Users;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace ConsentCollector.Business.Consent.Validators
{
    public sealed class CreateUserModelValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserModelValidator()
        {
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Length(3, 20)
                .Matches(@"^[a-zA-Z0-9]*$")
                .WithMessage("{PropertyName} should not contain special character(s)!");

            List<string> role = new List<string>() { "user", "admin" };


            RuleFor(x => x.Role)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Must(x => role.Contains(x))
                .WithMessage("Please only use: " + String.Join(",", role));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .MinimumLength(8).WithMessage("{PropertyName} length must be at least 8.")
                .MaximumLength(16).WithMessage("{PropertyName} length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("{PropertyName} must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("{PropertyName} must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("{PropertyName} must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("{PropertyName} must contain at least one (!? *.).");

        }
    }
}
