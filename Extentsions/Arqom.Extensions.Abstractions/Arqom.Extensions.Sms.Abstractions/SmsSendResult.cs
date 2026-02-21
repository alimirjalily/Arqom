namespace Arqom.Extensions.Sms.Abstractions;

public class SmsSendResult
{
    public bool Success { get; set; }
    public string? MessageId { get; set; }
    public string? ErrorMessage { get; set; }
}
