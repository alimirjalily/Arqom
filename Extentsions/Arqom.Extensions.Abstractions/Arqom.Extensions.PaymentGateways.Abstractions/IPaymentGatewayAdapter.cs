namespace Arqom.Extensions.PaymentGateways.Abstractions;

public interface IPaymentGatewayAdapter
{
    string Name { get; }

    /// <summary>
    /// ساخت لینک پرداخت 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PaymentResult> CreatePaymentAsync(PaymentCreateRequest request,CancellationToken cancellationToken = default);

    /// <summary>
    /// تایید پرداخت
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PaymentVerificationResult> VerifyPaymentAsync(PaymentVerifyRequest request,CancellationToken cancellationToken = default);

    /// <summary>
    /// بازگشت وجه (Refund) در صورت نیاز
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PaymentRefundResult> RefundPaymentAsync(PaymentRefundRequest request,CancellationToken cancellationToken = default);

    /// <summary>
    ///  گرفتن وضعیت تراکنش (مخصوص مواقعی که کاربر برنگشته به سایت یا برای مانیتورینگ)
    /// </summary>
    /// <param name="transactionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PaymentStatusResult> GetPaymentStatusAsync(string transactionId,CancellationToken cancellationToken = default);
}

