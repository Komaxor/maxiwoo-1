using FluentValidation;
using Microsoft.Extensions.Options;
using Mxc.IBSDiscountCard.Application.User.Commands;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginUser>
    {
        private readonly PolicyOptions _policyOptions;

        public LoginUserValidator(IOptions<PolicyOptions> options)
        {
            _policyOptions = options.Value;

            RuleFor(l => l.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Not a valid email address");
            RuleFor(l => l.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }
    }
}
