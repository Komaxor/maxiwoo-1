using FluentValidation;
using Mxc.IBSDiscountCard.Application.User.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.User.Validators
{
    public class ActivateUserValidator : AbstractValidator<ActivateUser>
    {
        public ActivateUserValidator()
        {
            RuleFor(u => u.ActivationCode).NotEmpty();
        }
    }
}
