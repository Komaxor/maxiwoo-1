using Mxc.Domain.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications
{
    /// <summary>
    /// Filter by username
    /// </summary>
    public class UserNameEquals : IFilterSpecification<User, IUserSpecificationVisitor>
    {
        public string UserName { get; }

        public UserNameEquals(string userName)
        {
            UserName = userName;
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
