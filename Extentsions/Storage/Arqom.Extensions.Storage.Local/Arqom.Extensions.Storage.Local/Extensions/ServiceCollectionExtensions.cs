using Arqom.Extensions.Storage.Abstractions;
using Arqom.Extensions.Storage.Local.Options;
using Arqom.Extensions.Storage.Local.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arqom.Extensions.Storage.Local.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LocalStorageOptions>(configuration.GetSection("Storage:Local"));
        services.AddTransient<IStorageAdapter, LocalStorageAdapter>();
        return services;
    }
}
