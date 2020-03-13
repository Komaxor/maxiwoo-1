using System;
using System.Collections.Generic;
using System.Transactions;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;

namespace Mxc.IBSDiscountCard.Application.User.Responses
{
    public class SubscriptionResponse
    {
        public virtual int? BillingDayOfMonth { get; protected set; }

        public virtual DateTime? BillingPeriodEndDate { get; protected set; }

        public virtual DateTime? BillingPeriodStartDate { get; protected set; }

        public virtual int? CurrentBillingCycle { get; protected set; }

        public virtual int? DaysPastDue { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual int? FailureCount { get; protected set; }

        public virtual DateTime? FirstBillingDate { get; protected set; }

        public virtual DateTime? CreatedAt { get; protected set; }

        public virtual DateTime? UpdatedAt { get; protected set; }

        public virtual bool? HasTrialPeriod { get; protected set; }

        public virtual string Id { get; protected set; }

        public virtual bool? NeverExpires { get; protected set; }

        public virtual decimal? NextBillAmount { get; protected set; }

        public virtual DateTime? NextBillingDate { get; protected set; }

        public virtual decimal? NextBillingPeriodAmount { get; protected set; }

        public virtual int? NumberOfBillingCycles { get; protected set; }

        public virtual DateTime? PaidThroughDate { get; protected set; }

        public virtual string PaymentMethodToken { get; protected set; }

        public virtual string PlanId { get; protected set; }

        public virtual decimal? Price { get; protected set; }

        public virtual int? TrialDuration { get; protected set; }

        public virtual string TrialDurationUnit { get; protected set; }

        public virtual string MerchantAccountId { get; protected set; }
    }
}