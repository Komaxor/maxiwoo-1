using FluentValidation;
using Mxc.IBSDiscountCard.Application.Place.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Place.Validators
{
    public class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            RuleFor(a => a.DisplayAddress).NotEmpty();
            RuleFor(a => a.Latitude).NotEmpty();
            RuleFor(a => a.Longitude).NotEmpty();
        }
    }
}
