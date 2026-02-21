namespace Arqom.Core.RequestResponse.Common;

public sealed class ApplicationMessage
{
    public string Code { get; }
    public MessageSeverity Severity { get; }
    public string?[] Args { get; }

    public ApplicationMessage(
        string code,
        MessageSeverity severity = MessageSeverity.Error,
        params string?[] args)
    {
        Code = code;
        Severity = severity;
        Args = args;
    }
}
