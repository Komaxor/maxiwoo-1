using Mxc.Domain.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications
{
    /// <summary>
    /// Filter by place tag
    /// </summary>
    public class PlaceTagsContains : IFilterSpecification<Place, IPlaceSpecificationVisitor>
    {
        public string Tag { get; }

        public PlaceTagsContains(string tag)
        {
            Tag = tag;
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
