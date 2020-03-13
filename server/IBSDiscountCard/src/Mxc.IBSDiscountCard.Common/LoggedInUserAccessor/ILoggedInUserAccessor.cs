using System;

namespace Mxc.IBSDiscountCard.Common.LoggedInUserAccessor
{
    /// <summary>
    /// Logged in user accessor
    /// </summary>
    public interface ILoggedInUserAccessor
    {
        /// <summary>
        /// Insistute id of current user
        /// </summary>
        Guid InstitueId { get; }
        
        /// <summary>
        /// UserName of current user
        /// </summary>
        string UserName { get; }
    }
}