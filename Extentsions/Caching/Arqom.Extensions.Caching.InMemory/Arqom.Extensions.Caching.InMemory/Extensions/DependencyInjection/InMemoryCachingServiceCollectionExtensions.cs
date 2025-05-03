using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.Caching.Abstractions;
using Arqom.Extensions.Caching.InMemory.Services;

namespace Arqom.Extensions.DependencyInjection;

public static class InMemoryCachingServiceCollectionExtensions
{
    public static IServiceCollection AddArqomInMemoryCaching(this IServiceCollection services)
        => services.AddMemoryCache().AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
}