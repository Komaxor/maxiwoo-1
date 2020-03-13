using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Mxc.IBSDiscountCard.Application.User.Responses;

namespace Mxc.IBSDiscountCard.Application.User.Services
{
    public interface IPaymentGateway
    {
        Task<string> CreateCustomerAsync(string fullname, string email);

        Task<string> GenerateClientTokenAsync(string customerId);

        Task<string> CreateSubscriptionAsync(string customerId, string paymentMethodNonce);
        Task HandleWebhookAsync(string signature, string payload);
        Task CancelSubscriptionAsync(string subscriptionId);
    }
}