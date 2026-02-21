namespace Arqom.Extensions.Payment.Abstractions;

public class PaymentRefundRequest
{
    public string TransactionId { get; set; } = string.Empty; // تراکنش اصلی
    public decimal Amount { get; set; } // مبلغ بازگشتی
}

