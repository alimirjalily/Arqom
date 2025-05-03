using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.Events.Abstractions;
using Arqom.Extensions.Events.PollingPublisher.Dal.Dapper.DataAccess;
using Arqom.Extensions.Events.PollingPublisher.Dal.Dapper.Options;

namespace Arqom.Extensions.DependencyInjection;
public static class PollingPublisherServiceCollectionExtensions
{
    public static IServiceCollection AddArqomPollingPublisherDalSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PollingPublisherDalRedisOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddArqomPollingPublisherDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddArqomPollingPublisherDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddArqomPollingPublisherDalSql(this IServiceCollection services, Action<PollingPublisherDalRedisOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IOutBoxEventItemRepository, SqlOutBoxEventItemRepository>();
    }
}