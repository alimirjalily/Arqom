namespace Arqom.Extensions.Sms.Abstractions;

public interface ISmsProvider
{
    public Task<SmsSendResult> SendAsync(SmsMessage message);
}

public class SmsMessage
{
    public string To { get; set; } = string.Empty;              
    public string Body { get; set; } = string.Empty ;         
    public SmsSignature? Signature { get; set; } 
}

public class SmsSignature
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}
