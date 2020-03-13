using Mxc.Domain.Abstractions.Specifications;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications
{
    /// <summary>
    /// Filter by subscription id
    /// </summary>
    public class ExternalSubscriptionId : IFilterSpecification<User, IUserSpecificationVisitor>
    {
        public string SubscriptionId { get; }
        
        public ExternalSubscriptionId(string subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }
        
        public void Accept(IUserSpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsSatisfiedBy(User obj)
        {
            throw new System.NotImplementedException();
        }
    }
}