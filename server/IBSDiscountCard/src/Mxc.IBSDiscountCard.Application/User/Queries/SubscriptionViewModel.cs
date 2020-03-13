using Mxc.IBSDiscountCard.Domain.UserAggregate;
using System;

namespace Mxc.IBSDiscountCard.Application.User.Queries
{
    public class SubscriptionViewModel
    {
        public SubscriptionTypeViewModel Type { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset FreeEndDate { get; set; }
        public DateTimeOffset? PremiumStartDate { get; set; }
        public DateTimeOffset? PremiumCancelDate { get; set; }
        public SubscriptonStatusViewModel Status { get; set; }
    }
}
