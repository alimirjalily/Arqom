namespace Arqom.Extensions.PaymentGateways.Abstractions;

public class PaymentRefundRequest
{
    public string TransactionId { get; set; } = default!;
    public decimal Amount { get; set; }
}
