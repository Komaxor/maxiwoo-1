﻿namespace Mxc.IBSDiscountCard.Common
{
    public enum FunctionCodes
    {
        UserNotLoggedInToGetDiscount,
        PhotoCannotEmpty,
        InsufficentPasswordStrenght,
        ActivationCodeShouldDifferent,
        WeakPassword,
        UploadPlaceImagePlaceNotFound,
        UploadPlaceImageUpdateNotFullfiled,
        UploadUserImageUserNotFound,
        UploadUserImageUpdateNotFullfiled,
        ImageNotFound,
        RegisterUserError,
        InvalidUserName,
        DuplicateUserName,
        UserLoginRefused,
        UserLoginLockedOutUser,
        UserLoginActivationNeeded,
        UserLoginRoleNotAddedToUser,
        PlaceNotFoundById,
        GetMyProfileNotFoundUser,
        GetMyProfileNotFoundInstitute,
        EmailSendingError,
        SendActivateCodeUserUserNotFound,
        SendActivateCodeAlreadyConfirmed,
        SendActivateCodeUpdateNotFullfiled,
        SendActivateEmailSendingError,
        ActivateUserUserNotFound,
        ActivateUserAlreadyConfirmed,
        ActivateUserWrongCode,
        ActivateUserUpdateNotFullfiled,
        ChangePasswordUserNotFound,
        ChangePasswordNewShouldDifferent,
        ChangePasswordError,
        ChangePasswordDisabledForaWhile,
        PasswordResetUserNotFound,
        PasswordResetDisabledForaWhile,
        PasswordResetEmailSendingError,
        PasswordResetUpdateNotFullfiled,
        PasswordResetClearUpdateNotFullfiled,
        SetNewPasswordInvalidCode,
        SetNewPasswordWeakPassword,
        SetNewPasswordChangePasswordError,
        SetNewPasswordUpdateNotFullfiled,
        SubscribeDenyByProvider,
        SubscribeErrorFromProvider,
        SubscribeUserNotFound,
        SubscribeUpdateNotFullfiled,
        SubscribeAlreadyPremium,
        UnsubscribeAlreadyCanceled,
        UnsubscribeErrorFromProvider,
        UnsubscribeUserNotFound,
        UnsubscribeUpdateNotFullfiled,
        UnsubscribeWrongUserInput,
        BrainTreeClientTokenGeneration,
        BraintreeCustomerCreation,
        BraintreePaymentMethodCreation,
        BraintreeSubscriptionCreation,
        UserAlreadyHasPremiumSubscription,
        BraintreeSubscriptionCancel,
        UnsubscribeUserIsNotPremium
    }
}
