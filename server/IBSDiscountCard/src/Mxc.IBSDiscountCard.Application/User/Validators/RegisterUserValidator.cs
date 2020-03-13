using FluentValidation;
using Microsoft.Extensions.Options;
using Mxc.IBSDiscountCard.Application.User.Commands;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        private readonly PolicyOptions _policyOptions;

        public RegisterUserValidator(IOptions<PolicyOptions> options)
        {
            _policyOptions = options.Value;

            RuleFor(c => c.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                //.Must(e => e.EndsWith("@ibs-b.hu"))
                .WithMessage("Not a valid email address");
            RuleFor(c => c.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MinimumLength(_policyOptions.PasswordMinLenght);
            RuleFor(c => c.FullName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateFullName)
                .WithMessage("Please enter your full name");
        }

        private bool ValidateFullName(string fullName)
        {
            var words = fullName.Trim().Split(' ');
            if (words.Length >= 2)
            {
                foreach (var word in words)
                {
                    if (word.Length < 2)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }
}
