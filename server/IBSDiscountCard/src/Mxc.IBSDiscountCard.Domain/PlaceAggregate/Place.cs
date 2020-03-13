using Mxc.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate
{
    /// <summary>
    /// Place entity
    /// </summary>
    public class Place : Entity<PlaceId, Guid>
    {
        /// <summary>
        /// Place is visible or not for the users
        /// </summary>
        public bool IsHidden { get; set; }
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
        public Address Address { get; private set; }

        private List<string> _tags;

        public IReadOnlyCollection<string> Tags
        {
            get => _tags == null ? null : new ReadOnlyCollection<string>(_tags);
            private set => _tags = value?.ToList();
        }

        public OpeningHoursOfDay OpeningHours { get; set; }

        public Place(string name, string type, string about, OpeningHoursOfDay openingHours, string previewImage, Address address, IReadOnlyCollection<string> tags, bool isHidden, int categoryId, string phoneNumber, string email, string web, string facebook, string instagram)
            : base(new PlaceId(Guid.NewGuid()))
        {
            Name = name;
            Type = type;
            About = about;
            OpeningHours = openingHours;
            PreviewImage = previewImage;
            Address = address;
            Tags = tags;
            IsHidden = isHidden;
            CategoryId = categoryId;
            PhoneNumber = phoneNumber;
            Email = email;
            Web = web;
            Facebook = facebook;
            Instagram = instagram;
        }

        public Place(PlaceId id, string name, string type, string about, OpeningHoursOfDay openingHours, string previewImage, Address address, IReadOnlyCollection<string> tags, bool isHidden, int categoryId, string phoneNumber, string email, string web, string facebook, string instagram)
         : base(id)
        {
            Name = name;
            Type = type;
            About = about;
            OpeningHours = openingHours;
            PreviewImage = previewImage;
            Address = address;
            Tags = tags;
            IsHidden = isHidden;
            CategoryId = categoryId;
            PhoneNumber = phoneNumber;
            Email = email;
            Web = web;
            Facebook = facebook;
            Instagram = instagram;
        }

        public void ModifyImage(string previewImage)
        {
            PreviewImage = previewImage;
        }

        public void Update(string name, string type, string about, OpeningHoursOfDay openingHours, Address address, IReadOnlyCollection<string> tags, bool isHidden, int categoryId, string phoneNumber, string email, string web, string facebook, string instagram)
        {
            Name = name;
            Type = type;
            About = about;
            OpeningHours = openingHours;
            Address = address;
            Tags = tags;
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
