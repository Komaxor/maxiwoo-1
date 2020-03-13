using System;
using Mxc.Domain.Abstractions.Entities;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate
{
    public class SubscriptionId : EntityGuid
    {
        public SubscriptionId(Guid id) : base(id)
        {
        }
    }
}