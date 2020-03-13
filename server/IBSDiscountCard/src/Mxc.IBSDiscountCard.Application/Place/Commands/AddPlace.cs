using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Place.Commands
{
    public class AddPlace : IIBSDiscountCardCommand<AddPlaceResponse>
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string About { get; private set; }
        public int CategoryId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Web { get; private set; }
        public string Facebook { get; private set; }
        public string Instagram { get; private set; }
        public string PreviewImage { get; private set; }
        public AddressDto Address { get; private set; }
        public List<string> Tags { get; private set; }
        public OpeningHoursOfDayDto OpeningHours { get; private set; }
        public bool IsHidden { get; private set; }

        public AddPlace(string name, string type, string about, string previewImage, AddressDto address, string tags, OpeningHoursOfDayDto openingHours, bool isHidden, int categoryId, string phoneNumber, string email, string web, string facebook, string instagram)
        {
            Name = name;
            Type = type;
            About = about;
            PreviewImage = previewImage;
            Address = address;
            Tags = tags?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
            OpeningHours = openingHours;
            IsHidden = isHidden;
            CategoryId = categoryId;
            PhoneNumber = phoneNumber;
            Email = email;
            Web = web;
            Facebook = facebook;
            Instagram = instagram;
        }

        public AddPlace()
        {
        }
    }

    public class AddressDto
    {
        public string DisplayAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public AddressDto(string displayAddress, string latitude, string longitude)
        {
            DisplayAddress = displayAddress;
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public class OpeningHoursOfDayDto
    {
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }

        public OpeningHoursOfDayDto(string monday, string tuesday, string wednesday, string thursday, string friday, string saturday, string sunday)
        {
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
        }
    }
}
