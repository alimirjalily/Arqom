using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.MessageBus.Abstractions;
using Arqom.Extensions.MessageBus.MessageInbox;
using Arqom.Extensions.MessageBus.MessageInbox.Options;

namespace Arqom.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddArqomMessageInbox(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageInboxOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddArqomMessageInbox(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddArqomMessageInbox(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddArqomMessageInbox(this IServiceCollection services, Action<MessageInboxOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IMessageConsumer, InboxMessageConsumer>();
    }
}