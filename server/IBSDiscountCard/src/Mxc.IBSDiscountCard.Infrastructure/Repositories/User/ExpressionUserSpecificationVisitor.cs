using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.User
{
    public class ExpressionUserSpecificationVisitor : ExpressionVisitor<UserDb, ExpressionUserSpecificationVisitor, IUserSpecificationVisitor,
        Domain.UserAggregate.User>, IUserSpecificationVisitor
    {
        public void Visit(IdEquals spec)
        {
            FilterExpression = db => db.Id == spec.UserId.Id;
        }

        public void Visit(UserNameEquals spec)
        {
            FilterExpression = db => db.UserName == spec.UserName;
        }

        public void Visit(ExternalSubscriptionId spec)
        {
        }
    }
}
