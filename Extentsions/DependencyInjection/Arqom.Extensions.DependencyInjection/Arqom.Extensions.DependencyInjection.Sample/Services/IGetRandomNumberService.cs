using Arqom.Extensions.DependencyInjection.Abstractions;

namespace Arqom.Extensions.DependencyInjection.Sample.Services;

public interface IGetGuidTransientService : ITransientLifetime
{
    Guid Execute();
}