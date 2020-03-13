using System;
using Microsoft.AspNetCore.Identity;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.User
{
    public class UserDb : IdentityUser<Guid>
    {
        [PersonalData]
        public string FullName { get; set; }

        public SubscriptionDb Subscription { get; set; }
        public string ProfilePhoto { get; set; }
        public Guid InstitudeId { get; set; }
        public string ActivationCode { get; set; }
        public int PasswordChangeFailedCount { get; set; }
        public DateTimeOffset? PasswordChangeLockoutEnd { get; set; }
        public string PasswordResetCode { get; set; }
        public DateTimeOffset? PasswordResetLockEnd { get; set; }
        public string CustomerId { get; set; }
    }

    public class SubscriptionDb
    {
        public Guid Id { get; set; }
        public SubscriptionTypeDb Type { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset FreeEndDate { get; set; }
        public DateTimeOffset? PremiumStartDate { get; set; }
        public DateTimeOffset? PremiumCancelDate { get; set; }
        public string ExternalSubscriptionId { get; set; }
        public string ErrorMessage { get; set; }
        public SubscriptionStatusDb Status { get; set; }
    }
}