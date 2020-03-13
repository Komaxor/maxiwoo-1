using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.InstituteAggregate
{
    /// <summary>
    /// Institute entity visitor
    /// </summary>
    public interface IInstituteSpecificationVisitor : ISpecificationVisitor<IInstituteSpecificationVisitor, Institute>
    {
        void Visit(IdEquals spec);
    }
}
