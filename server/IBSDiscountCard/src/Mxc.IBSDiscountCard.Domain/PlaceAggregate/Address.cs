using Mxc.Domain.Abstractions.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate
{
    /// <summary>
    /// Place address data with coordinates
    /// </summary>
    public class Address : ValueObject
    {
        public string DisplayAddress { get; }
        public string Latitude { get; }
        public string Longitude { get; }

        public Address(string displayAddress, string latitude, string longitude)
        {
            DisplayAddress = displayAddress;
            Latitude = latitude;
            Longitude = longitude;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DisplayAddress;
            yield return Latitude;
            yield return Longitude;
        }
    }
}
