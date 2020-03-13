namespace Mxc.IBSDiscountCard.Common
{
    public class PaymentOptions
    {
        public string CountryCode { get; set; }
        
        public string CurrencyCode { get; set; }
        
        public string MerchantName { get; set; }

        public string Environment { get; set; }

        public string MerchantId { get; set; }
        public string MerchantAccountId { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }
        public string PlanId { get; set; }
    }
}