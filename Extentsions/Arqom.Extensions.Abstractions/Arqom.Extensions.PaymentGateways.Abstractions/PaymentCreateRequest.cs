namespace Arqom.Extensions.PaymentGateways.Abstractions;

public class PaymentCreateRequest
{
    public string OrderId { get; set; } = default!;
    public decimal Amount { get; set; }
    public string Description { get; set; } = default!;
    public string CallbackUrl { get; set; } = default!;
    public string? PayerEmail { get; set; }
    public string? PayerPhone { get; set; }

    // فیلدهای اضافی خاص درگاه‌ها
    public Dictionary<string, string>? AdditionalData { get; set; }
}
