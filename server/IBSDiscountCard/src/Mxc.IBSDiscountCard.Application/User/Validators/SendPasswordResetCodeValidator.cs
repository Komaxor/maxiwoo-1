using FluentValidation;
using Mxc.IBSDiscountCard.Application.User.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Validators
{
    public class SendPasswordResetCodeValidator : AbstractValidator<SendPasswordResetCode>
    {
        public SendPasswordResetCodeValidator()
        {
            RuleFor(c => c.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
