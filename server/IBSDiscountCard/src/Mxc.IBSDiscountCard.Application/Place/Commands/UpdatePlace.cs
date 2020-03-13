using System;
using System.Collections.Generic;
using System.Linq;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.Place.Queries;

namespace Mxc.IBSDiscountCard.Application.Place.Commands
{
    public class UpdatePlace : IIBSDiscountCardCommand<UpdatePlaceResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public int CategoryId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Web { get; private set; }
        public string Facebook { get; private set; }
        public string Instagram { get; private set; }
        public string About { get; private set; }
        public string PreviewImage { get; private set; }
        public AddressDto Address { get; private set; }
        public List<string> Tags { get; private set; }
        public OpeningHoursOfDayDto OpeningHours { get; private set; }
        public bool IsHidden { get; private set; }

        // Automapper ctor
        public UpdatePlace()
        {
        }
        
        public UpdatePlace(string name, string type, string about, string previewImage, AddressDto address, string tags, OpeningHoursOfDayDto openingHours, bool isHidden, int categoryId, string phoneNumber, string email, string web, string facebook, string instagram)
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
    }
}