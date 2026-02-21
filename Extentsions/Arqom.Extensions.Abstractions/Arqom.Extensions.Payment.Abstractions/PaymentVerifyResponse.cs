namespace Arqom.Extensions.Payment.Abstractions;

public class PaymentVerifyResponse
{
    public bool Success { get; set; }
    public string TrackingCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

