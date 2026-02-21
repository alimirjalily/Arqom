namespace Arqom.Core.RequestResponse.Common;
public abstract class ApplicationServiceResult : IApplicationServiceResult
{
    protected readonly List<ApplicationMessage> _messages = new();

    public IEnumerable<ApplicationMessage> Messages => _messages;

    public ApplicationServiceStatus Status { get; set; }

    public void AddMessage(ApplicationMessage message) => _messages.Add(message);
    public void AddMessages(IEnumerable<ApplicationMessage> messages) => _messages.AddRange(messages);

    public bool HasError =>
        _messages.Any(m => m.Severity == MessageSeverity.Error);

    public void ClearMessages() => _messages.Clear();

}
