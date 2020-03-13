using FluentValidation;
using Mxc.IBSDiscountCard.Application.Place.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Place.Validators
{
    public class AddPlaceValidator : AbstractValidator<AddPlace>
    {
        public AddPlaceValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.CategoryId).NotEmpty();
            RuleFor(c => c.Address).SetValidator(new AddressValidator());
        }
    }
}
