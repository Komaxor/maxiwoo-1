using System;
using Mxc.Domain.Abstractions.Entities;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate
{
    public class UserId : EntityGuid
    {
        public UserId(Guid id) : base(id)
        {
        }
    }
}