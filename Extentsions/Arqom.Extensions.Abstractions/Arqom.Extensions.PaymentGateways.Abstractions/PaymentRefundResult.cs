namespace Arqom.Extensions.PaymentGateways.Abstractions;

public class PaymentRefundResult
{
    public bool Success { get; set; }
    public string? RefundTransactionId { get; set; }
    public string? ErrorKey { get; set; }
    public decimal? RefundedAmount { get; set; }
    public string? Status { get; set; }
}
