namespace Arqom.Extensions.Payment.Abstractions;

public class PaymentGatewaySetting
{
    public string ProviderName { get; set; } = string.Empty; // "Zarinpal", "Stripe", "PayPal"
    public string MerchantId { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string CallbackBaseUrl { get; set; } = string.Empty; // مثلاً https://mysite.com/payment/callback

    public string CallbackParamName { get; set; } = "referenceId";
}

