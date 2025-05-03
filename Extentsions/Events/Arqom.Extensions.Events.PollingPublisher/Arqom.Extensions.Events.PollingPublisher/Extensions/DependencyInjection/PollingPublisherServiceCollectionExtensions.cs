using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.Events.PollingPublisher;
using Arqom.Extensions.Events.PollingPublisher.Options;

namespace Arqom.Extensions.DependencyInjection;

public static class PollingPublisherServiceCollectionExtensions
{
    public static IServiceCollection AddArqomPollingPublisher(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PollingPublisherOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddArqomPollingPublisher(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddArqomPollingPublisher(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddArqomPollingPublisher(this IServiceCollection services, Action<PollingPublisherOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddHostedService<PoolingPublisherBackgroundService>();
    }
}