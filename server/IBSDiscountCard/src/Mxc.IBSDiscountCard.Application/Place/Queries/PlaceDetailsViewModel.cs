using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Place.Queries
{
    public class PlaceDetailsViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string About { get; set; }
        public int CategoryId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string PreviewImage { get; set; }
        public AddressViewModel Address { get; set; }
        public List<string> Tags { get; set; }
        public OpeningHoursViewModel OpeningHours { get; set; }
    }

    public class AddressViewModel
    {
        public string DisplayAddress { get; set; }

        [RegularExpression(@"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$", ErrorMessage = "Not a valid latitude")]
        public string Latitude { get; set; }

        [RegularExpression(@"^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$", ErrorMessage = "Not a valid longitude")]
        public string Longitude { get; set; }
    }

    public class OpeningHoursViewModel
    {
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}
