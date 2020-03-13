using Mxc.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Mxc.IBSDiscountCard.Common;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate
{
    /// <summary>
    /// Subscription entity
    /// </summary>
    public class Subscription : Entity<SubscriptionId, Guid>
    {
        public SubscriptionType Type { get; private set; }
        public DateTimeOffset CreatedDate { get; }
        public DateTimeOffset FreeEndDate { get; }
        public DateTimeOffset? PremiumStartDate { get; private set; }
        public DateTimeOffset? PremiumCancelDate { get; private set; }
        
        /// <summary>
        /// Payment provider external id
        /// </summary>
        public string ExternalSubscriptionId { get; private set; }
        public string ErrorMessage { get; private set; }
        public SubscriptonStatus Status { get; private set; }

        /// <summary>
        /// Load existing subscription
        /// </summary>
        public Subscription(SubscriptionId id, SubscriptionType type, DateTimeOffset createdDate,
            DateTimeOffset freeEndDate, DateTimeOffset? premiumStartDate, DateTimeOffset? premiumCancelDate,
            string externalSubscriptionId, string errorMessage, SubscriptonStatus status) : base(id)
        {
            Type = type;
            CreatedDate = createdDate;
            FreeEndDate = freeEndDate;
            PremiumStartDate = premiumStartDate;
            PremiumCancelDate = premiumCancelDate;
            ExternalSubscriptionId = externalSubscriptionId;
            ErrorMessage = errorMessage;
            Status = status;
        }

        /// <summary>
        /// Create new active basic subscription
        /// </summary>
        /// <param name="createdDate"></param>
        public Subscription(DateTimeOffset createdDate) : base(new SubscriptionId(Guid.NewGuid()))
        {
            Type = SubscriptionType.Basic;
            CreatedDate = createdDate;
            Status = SubscriptonStatus.Active;
        }

        public bool CanUpgradeToPremium()
        {
            return Type != SubscriptionType.Premium;
        }

        /// <summary>
        /// Upgrade basic subscription to Premium
        /// </summary>
        public void UpgradeToPremium(DateTimeOffset startDate, string externalSubscriptionId)
        {
            if (!CanUpgradeToPremium())
            {
                throw new IBSDiscountCardDomainException(FunctionCodes.SubscribeAlreadyPremium,
                    "subscription_is_already_premium");
            }

            Status = SubscriptonStatus.NotPaid;
            Type = SubscriptionType.Premium;
            ExternalSubscriptionId = externalSubscriptionId;
            PremiumStartDate = startDate;
        }

        public bool CanCancelPremium()
        {
            return Type == SubscriptionType.Premium;
        }
        
        public void CancelPremium(DateTimeOffset cancelDate)
        {
            if (!CanCancelPremium())
            {
                throw new IBSDiscountCardDomainException(FunctionCodes.UnsubscribeAlreadyCanceled,
                    "subscription_is_not_premium");
            }

            Status = SubscriptonStatus.Active;
            Type = SubscriptionType.Basic;
            ExternalSubscriptionId = null;
            PremiumStartDate = null;
        }
    }
}