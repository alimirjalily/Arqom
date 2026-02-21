namespace Arqom.Extensions.Payment.Abstractions;

public class PaymentRequest
{
    public decimal Amount { get; set; }

    public string Currency { get; set; } = "IRR";

    public int MyProperty { get; set; }

    public string CallbackUrl { get; set; } = default!;

    public string ReferenceId { get; set; } = string.Empty;
    public string Description { get; set; } = default!;
}

