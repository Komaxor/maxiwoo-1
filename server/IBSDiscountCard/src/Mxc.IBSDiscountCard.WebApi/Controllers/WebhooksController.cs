using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mxc.IBSDiscountCard.Application.User.Services;

namespace Mxc.IBSDiscountCard.WebApi.Controllers
{
    public class WebhooksController : ApiControllerBase
    {
        private readonly IPaymentGateway _paymentGateway;

        public WebhooksController(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }

        /// <summary>
        /// Webhook callback endpoint for payment provider
        /// </summary>
        [HttpPost("accept")]
        public async Task<IActionResult> AcceptAsync()
        {
            await _paymentGateway.HandleWebhookAsync(Request.Form["bt_signature"],
                Request.Form["bt_payload"]);

            return Ok();
        }
    }
}