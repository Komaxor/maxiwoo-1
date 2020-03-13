using Mxc.Domain.Abstractions.Repositories;
using Mxc.Domain.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate
{
    /// <summary>
    /// User data repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get user by filter
        /// </summary>
        /// <param name="specification">User filter</param>
        /// <returns>User</returns>
        Task<User> GetAsync(IFilterSpecification<User, IUserSpecificationVisitor> specification);
        
        /// <summary>
        /// Update specific user
        /// </summary>
        /// <param name="item">User data</param>
        /// <param name="guardSpecification">Guard close update only happens if guard is true</param>
        /// <returns>Result of the update</returns>
        Task<bool> UpdateAsync(User item, IFilterSpecification<User, IUserSpecificationVisitor> guardSpecification = null);
    }
}
