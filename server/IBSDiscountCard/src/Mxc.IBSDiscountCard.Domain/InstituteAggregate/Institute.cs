using Mxc.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.InstituteAggregate
{
    /// <summary>
    /// Institute entity
    /// </summary>
    public class Institute : Entity<InstituteId, Guid>
    {
        public string Name { get; private set; }

        public Institute(string name) : base(new InstituteId(Guid.NewGuid()))
        {
            Name = name;
        }

        public Institute(InstituteId id, string name) : base(id)
        {
            Name = name;
        }
    }
}
