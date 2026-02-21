namespace Arqom.Extensions.Payment.Abstractions;

public class PaymentVerifyRequest
{
    public string TransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string ReferenceId { get; set; } = string.Empty;
}

        