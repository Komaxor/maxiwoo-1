using System;
using System.Collections.Generic;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.Place.Models
{
    public class PlaceDb
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string About { get; set; }
        public string PreviewImage { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public AddressDb Address { get; set; }
        public string[] Tags { get; set; }
        public OpeningHoursOfDayDb OpeningHours { get; set; }
        public bool IsHidden { get; set; }
        public int CategoryId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
    }

    public class AddressDb
    {
        public Guid Id { get; set; }
        public string DisplayAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class OpeningHoursOfDayDb
    {
        public Guid Id { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}