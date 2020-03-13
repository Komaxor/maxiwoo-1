using Mxc.Domain.Abstractions.Specifications;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications
{
    /// <summary>
    /// Filter by place name with contains logic
    /// </summary>
    public class PlaceNameContains : IFilterSpecification<Place, IPlaceSpecificationVisitor>
    {
        public string Name { get;}

        public PlaceNameContains(string name)
        {
            Name = name;
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