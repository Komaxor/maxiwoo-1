using System;
using Mxc.Domain.Abstractions.Specifications;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications
{
    /// <summary>
    /// Not hidden place filter
    /// </summary>
    public class NotHidden : IFilterSpecification<Place, IPlaceSpecificationVisitor>
    {
        public NotHidden()
        {
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