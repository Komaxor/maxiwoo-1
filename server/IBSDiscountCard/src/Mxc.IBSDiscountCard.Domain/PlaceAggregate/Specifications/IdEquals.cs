using System;
using Mxc.Domain.Abstractions.Specifications;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications
{
    /// <summary>
    /// Filter by id
    /// </summary>
    public class IdEquals : IFilterSpecification<Place, IPlaceSpecificationVisitor>
    {
        public PlaceId PlaceId { get; }

        public IdEquals(PlaceId placeId)
        {
            PlaceId = placeId;
        }
        
        public void Accept(IPlaceSpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsSatisfiedBy(Place obj)
        {
            throw new System.NotImplementedException();
        }
    }
}