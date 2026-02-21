namespace Arqom.Extensions.Payment.Abstractions;

public class PaymentStatusResponse
{
    public PaymentStatus Status { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public string TrackingCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
