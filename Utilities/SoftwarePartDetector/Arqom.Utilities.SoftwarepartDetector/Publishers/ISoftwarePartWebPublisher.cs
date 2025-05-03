using Arqom.Utilities.SoftwarePartDetector.DataModel;

namespace Arqom.Utilities.SoftwarePartDetector.Publishers;

public interface ISoftwarePartPublisher
{
    Task PublishAsync(SoftwarePart softwarePart);
}