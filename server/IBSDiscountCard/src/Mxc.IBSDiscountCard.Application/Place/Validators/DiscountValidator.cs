using FluentValidation;
using Mxc.IBSDiscountCard.Application.Place.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Place.Validators
{
    public class DiscountValidator : AbstractValidator<DiscountDto>
    {
        public DiscountValidator()
        {
            RuleFor(d => d.InstitudeId).NotEmpty();
            RuleFor(d => d.BasicDiscountDescription).NotEmpty();
            RuleFor(d => d.PremiumDiscountDescription).NotEmpty();
            RuleFor(d => d.MaxBasicDiscount).NotEmpty();
            RuleFor(d => d.MaxPremiumDiscount).NotEmpty();
        }
    }
}
