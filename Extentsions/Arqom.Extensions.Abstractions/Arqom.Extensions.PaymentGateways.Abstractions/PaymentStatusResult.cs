namespace Arqom.Extensions.PaymentGateways.Abstractions;

public class PaymentStatusResult
{
    public bool Success { get; set; }

    public string Status { get; set; } = default!;

    public string TransactionId { get; set; } = default!;

    public decimal Amount { get; set; }

    public string? ErrorKey { get; set; }

    public DateTime? TransactionDate { get; set; }
}
