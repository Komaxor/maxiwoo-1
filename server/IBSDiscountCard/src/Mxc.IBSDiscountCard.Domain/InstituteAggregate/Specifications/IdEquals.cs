using Mxc.Domain.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.InstituteAggregate.Specifications
{
    /// <summary>
    /// Institute id filter
    /// </summary>
    public class IdEquals : IFilterSpecification<Institute, IInstituteSpecificationVisitor>
    {
        public InstituteId InstituteId { get; }

        public IdEquals(InstituteId instituteId)
        {
            InstituteId = instituteId;
        }

        public void Accept(IInstituteSpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsSatisfiedBy(Institute obj)
        {
            throw new NotImplementedException();
        }
    }
}
