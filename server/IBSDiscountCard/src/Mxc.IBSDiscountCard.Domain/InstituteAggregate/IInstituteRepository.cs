using Mxc.Domain.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Domain.InstituteAggregate
{
    /// <summary>
    /// Institute repositroy
    /// </summary>
    public interface IInstituteRepository
    {
        /// <summary>
        /// Get an institute by filter
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        Task<Institute> GetAsync(IFilterSpecification<Institute, IInstituteSpecificationVisitor> specification);
    }
}
