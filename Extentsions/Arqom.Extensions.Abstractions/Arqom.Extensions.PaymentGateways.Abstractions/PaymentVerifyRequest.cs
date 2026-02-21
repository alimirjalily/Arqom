namespace Arqom.Extensions.PaymentGateways.Abstractions;

public class PaymentVerifyRequest
{
    public string OrderId { get; set; } = default!;
    public string TrackingCode { get; set; } = default!;
    public decimal Amount { get; set; }
    public Dictionary<string, string>? AdditionalData { get; set; }
}
