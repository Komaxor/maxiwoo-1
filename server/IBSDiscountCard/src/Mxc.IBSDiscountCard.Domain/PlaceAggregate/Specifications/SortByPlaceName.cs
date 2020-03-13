using Mxc.Domain.Abstractions.Specifications;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications
{
    /// <summary>
    /// Sort by place name
    /// </summary>
    public class SortByPlaceName : ISorterSpecification<Place, IPlaceSpecificationVisitor>
    {
        public SortByPlaceName(bool isAsc)
        {
            IsAsc = isAsc;
        }
        
        public void Accept(IPlaceSpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsAsc { get; }
    }
}