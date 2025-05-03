using Arqom.Extensions.DependencyInjection.Abstractions;

namespace Arqom.Extensions.DependencyInjection.Sample.Services;

public interface IGetGuidSingletoneService : ISingletoneLifetime
{
    Guid Execute();
}
