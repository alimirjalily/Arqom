namespace Arqom.Extensions.PaymentGateways.Zarinpal;

public class ZarinpalOptions
{
    public string MerchantId { get; set; } = default!;
    public string GatewayUrl { get; set; } = "https://sandbox.zarinpal.com/pg/v4";
    public string PaymentUrl { get; set; } = "https://www.zarinpal.com/pg/StartPay/";

    public string CallBackUrl { get; set; } = default!;
    public bool IsSandbox { get; set; } = false; // برای حالت تستی
}
