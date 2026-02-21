namespace Arqom.Extensions.PaymentGateways.Abstractions;

public class PaymentVerificationResult
{
    public bool IsSuccess { get; set; }
    public string? ReferenceId { get; set; }
    public string? ErrorKey { get; set; }
}
