using Mxc.Domain.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications
{
    /// <summary>
    /// Filter by place type
    /// </summary>
    public class PlaceTypeContains : IFilterSpecification<Place, IPlaceSpecificationVisitor>
    {
        public string Type { get; }

        public PlaceTypeContains(string type)
        {
            Type = type;
        }

        public void Accept(IPlaceSpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsSatisfiedBy(Place obj)
        {
            throw new NotImplementedException();
        }
    }
}
