using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate
{
    /// <summary>
    /// Visitor interface for the user entity
    /// </summary>
    public interface IUserSpecificationVisitor : ISpecificationVisitor<IUserSpecificationVisitor, User>
    {
        void Visit(IdEquals spec);
        void Visit(UserNameEquals spec);
        void Visit(ExternalSubscriptionId spec);
    }
}
