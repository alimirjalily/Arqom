using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.MessageBus.Abstractions;
using Arqom.Extensions.MessageBus.MessageInbox.Dal.Dapper;
using Arqom.Extensions.MessageBus.MessageInbox.Dal.Dapper.Options;

namespace Arqom.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddArqomMessageInboxDalSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageInboxDalDapperOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddArqomMessageInboxDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddArqomMessageInboxDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddArqomMessageInboxDalSql(this IServiceCollection services, Action<MessageInboxDalDapperOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IMessageInboxItemRepository, SqlMessageInboxItemRepository>();
    }
}