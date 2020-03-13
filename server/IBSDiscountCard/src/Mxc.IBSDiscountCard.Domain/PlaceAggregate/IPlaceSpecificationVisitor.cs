using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate
{
    /// <summary>
    /// Place entity visitor interface
    /// </summary>
    public interface IPlaceSpecificationVisitor : ISpecificationVisitor<IPlaceSpecificationVisitor, Place>
    {
        void Visit(IdEquals spec);
        void Visit(PlaceNameContains spec);
        void Visit(PlaceTypeContains spec);
        void Visit(PlaceTagsContains spec);
        void Visit(SortByPlaceName spec);
        void Visit(NotHidden spec);
    }
}