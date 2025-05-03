using Arqom.Extensions.DependencyInjection.Abstractions;

namespace Arqom.Extensions.DependencyInjection.Sample.Services;

public interface IGetGuidScopeService : IScopeLifetime
{
    Guid Execute();
}
