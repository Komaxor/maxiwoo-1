using FluentValidation;
using Microsoft.Extensions.Options;
using Mxc.IBSDiscountCard.Application.User.Commands;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Validators
{
    public class ChangeMyPasswordValidator : AbstractValidator<ChangeMyPassword>
    {
        private readonly PolicyOptions _policyOptions;

        public ChangeMyPasswordValidator(IOptions<PolicyOptions> options)
        {
            _policyOptions = options.Value;

            RuleFor(c => c.NewPassword)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MinimumLength(_policyOptions.PasswordMinLenght);
            RuleFor(c => c.OldPassword)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }
    }
}
