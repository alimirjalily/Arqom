namespace Arqom.Core.RequestResponse.Common;
public interface IApplicationServiceResult
{
    IEnumerable<ApplicationMessage> Messages { get; }
    ApplicationServiceStatus Status { get; set; }
}
