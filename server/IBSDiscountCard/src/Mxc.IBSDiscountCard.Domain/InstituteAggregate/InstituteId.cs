using System;
using Mxc.Domain.Abstractions.Entities;

namespace Mxc.IBSDiscountCard.Domain.InstituteAggregate
{
    public class InstituteId : EntityGuid
    {
        public InstituteId(Guid id) : base(id)
        {
        }
    }
}