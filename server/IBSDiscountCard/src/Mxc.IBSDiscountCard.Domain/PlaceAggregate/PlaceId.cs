using Mxc.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate
{
    public class PlaceId : EntityGuid
    {
        public PlaceId(Guid id) : base(id)
        {
        }
    }
}
