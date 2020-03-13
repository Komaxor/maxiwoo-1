using Mxc.Domain.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications
{
    /// <summary>
    /// Filter by user id
    /// </summary>
    public class IdEquals : IFilterSpecification<User, IUserSpecificationVisitor>
    {
        public UserId UserId { get; }

        public IdEquals(UserId userId)
        {
            UserId = userId;
        }

        public void Accept(IUserSpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsSatisfiedBy(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
