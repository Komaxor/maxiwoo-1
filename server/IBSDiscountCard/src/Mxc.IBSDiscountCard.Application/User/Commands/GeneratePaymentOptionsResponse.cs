namespace Mxc.IBSDiscountCard.Application.User.Commands
{
    public class GeneratePaymentOptionsResponse
    {
        public string ClientToken { get; set; }
        public string MerchantIdentifier { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string MerchantName { get; set; }
    }
}