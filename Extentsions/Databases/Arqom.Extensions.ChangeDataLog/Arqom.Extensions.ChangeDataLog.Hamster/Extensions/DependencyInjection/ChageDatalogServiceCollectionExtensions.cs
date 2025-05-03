using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.ChangeDataLog.Sql.Options;

namespace Arqom.Extensions.DependencyInjection;

public static class ChageDatalogServiceCollectionExtensions
{
    public static IServiceCollection AddArqomHamsterChageDatalog(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ChangeDataLogHamsterOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddArqomHamsterChageDatalog(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddArqomHamsterChageDatalog(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddArqomHamsterChageDatalog(this IServiceCollection services, Action<ChangeDataLogHamsterOptions> setupAction)
    {
        services.Configure(setupAction);
        return services;
    }
}