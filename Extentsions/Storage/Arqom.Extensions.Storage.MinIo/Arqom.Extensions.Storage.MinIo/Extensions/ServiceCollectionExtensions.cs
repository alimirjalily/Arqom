using Arqom.Extensions.Storage.Abstractions;
using Arqom.Extensions.Storage.MinIo.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arqom.Extensions.Storage.MinIo.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMinIoStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinIoStorageOptions>(configuration.GetSection("Storage:MinIo"));
        services.AddSingleton<IStorageAdapter, MinIoStorageAdapter>();

        return services;
    }
}
