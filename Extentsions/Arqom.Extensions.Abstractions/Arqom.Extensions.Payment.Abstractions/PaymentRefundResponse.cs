namespace Arqom.Extensions.Payment.Abstractions;

public class PaymentRefundResponse
{
    public bool Success { get; set; }
    public string RefundId { get; set; } = string.Empty; // شناسه بازگشت وجه سمت درگاه
    public string Message { get; set; } = string.Empty;
}