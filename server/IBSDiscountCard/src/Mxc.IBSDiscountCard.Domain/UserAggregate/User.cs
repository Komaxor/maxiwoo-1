using Mxc.Domain.Abstractions.Entities;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.UserAggregate
{
    public class User : Entity<UserId, Guid>
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Subscription Subscription { get; private set; }
        public string ProfilePhoto { get; private set; }
        public InstituteId InstituteId { get; private set; }
        public string ActivationCode { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string CustomerId { get; set; }

        public User(string fullName, string email, string password, string profilePhoto, InstituteId instituteId, DateTimeOffset createdDate, string customerId) : base(new UserId(Guid.NewGuid()))
        {
            FullName = fullName;
            Email = email;
            Password = password;
            ProfilePhoto = profilePhoto;
            InstituteId = instituteId;
            CustomerId = customerId;
            Subscription = new Subscription(createdDate);
        }

        public User(UserId id, string fullName, string email, string password, Subscription subscription, string profilePhoto, InstituteId instituteId, string activationCode, bool emailConfirmed, string customerId) : base(id)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Subscription = subscription;
            ProfilePhoto = profilePhoto;
            InstituteId = instituteId;
            ActivationCode = activationCode;
            EmailConfirmed = emailConfirmed;
            CustomerId = customerId;
        }

        public void ModifyPassword(string password)
        {
            if (password.Length < 5)
            {
                throw new IBSDiscountCardDomainException(FunctionCodes.InsufficentPasswordStrenght, "Password should be longer than 5 character.");
            }

            Password = password;
        }

        public void UpdatePhoto(string photo)
        {
            if (string.IsNullOrWhiteSpace(photo))
            {
                throw new IBSDiscountCardDomainException(FunctionCodes.PhotoCannotEmpty, "Photo cannot be empty.");
            }

            ProfilePhoto = photo;
        }

        public void SubscribeToPremium(DateTimeOffset startDate, string externalSubscriptionId)
        {
            if (string.IsNullOrWhiteSpace(externalSubscriptionId) || string.IsNullOrWhiteSpace(CustomerId))
            {
                throw new IBSDiscountCardDomainException(FunctionCodes.SubscribeDenyByProvider, "Subscribe denied by online payment provider.");
            }

            Subscription.UpgradeToPremium(startDate, externalSubscriptionId);
        }

        public void CancelPremiumSubscription(DateTimeOffset cancelDate)
        {
            Subscription.CancelPremium(cancelDate);
        }

        public void ModifyActivationCode(string code)
        {
            if (ActivationCode == code)
            {
                throw new IBSDiscountCardDomainException(FunctionCodes.ActivationCodeShouldDifferent, "Activation code should be different than current one.");
            }

            ActivationCode = code;
        }

        public void Activate()
        {
            EmailConfirmed = true;
            ActivationCode = "";
        }
    }
}
