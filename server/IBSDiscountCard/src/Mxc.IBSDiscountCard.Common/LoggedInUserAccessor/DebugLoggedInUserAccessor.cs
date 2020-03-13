using System;

namespace Mxc.IBSDiscountCard.Common.LoggedInUserAccessor
{
    /// <summary>
    /// Logged in user implementation for local testing
    /// </summary>
    public class DebugLoggedInUserAccessor : ILoggedInUserAccessor
    {
        public Guid InstitueId { get; } = MockData.InstitudeId;
        public string UserName { get; } = "test@ibs-b.hu";
    }
}