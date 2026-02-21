namespace Arqom.Extensions.PaymentGateways.Abstractions;

public class PaymentResult
{
    public bool IsSuccess { get; set; }
    public string? RedirectUrl { get; set; }
    public string TrackingCode { get; set; } = default!;
    public string? ErrorKey { get; set; }
}