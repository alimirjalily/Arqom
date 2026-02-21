namespace Arqom.Extensions.Payment.Abstractions;

public class PaymentResponse
{
    public bool Success { get; set; }
    public string PaymentUrl { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

