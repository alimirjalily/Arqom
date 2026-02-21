namespace Arqom.Extensions.Payment.Abstractions;

public interface IPaymentProvider
{
    string Name { get; } // مثلا "Zarinpal" یا "Stripe"

    Task<PaymentResponse> CreatePaymentAsync(
        PaymentRequest request,
         PaymentGatewaySetting setting,
        CancellationToken cancellationToken = default);
    Task<PaymentVerifyResponse> VerifyPaymentAsync(
        PaymentVerifyRequest request,
         PaymentGatewaySetting setting,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// بازگشت وجه (Refund) در صورت نیاز
    /// </summary>
    Task<PaymentRefundResponse> RefundPaymentAsync(
        PaymentRefundRequest request,
        PaymentGatewaySetting setting,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// گرفتن وضعیت تراکنش (مخصوص مواقعی که کاربر برنگشته به سایت یا برای مانیتورینگ)
    /// </summary>
    Task<PaymentStatusResponse> GetPaymentStatusAsync(
        string transactionId,
        PaymentGatewaySetting setting,
        CancellationToken cancellationToken = default);
}

