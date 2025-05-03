using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.ChangeDataLog.Abstractions;
using Arqom.Extensions.ChangeDataLog.Sql;
using Arqom.Extensions.ChangeDataLog.Sql.Options;

namespace Arqom.Extensions.DependencyInjection;

public static class ChageDatalogServiceCollectionExtensions
{
    public static IServiceCollection AddArqomChageDatalogDalSql(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddScoped<IEntityChageInterceptorItemRepository, DapperEntityChangeInterceptorItemRepository>();
        services.Configure<ChangeDataLogSqlOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddArqomChageDatalogDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddArqomChageDatalogDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddArqomChageDatalogDalSql(this IServiceCollection services, Action<ChangeDataLogSqlOptions> setupAction)
    {
        services.AddScoped<IEntityChageInterceptorItemRepository, DapperEntityChangeInterceptorItemRepository>();
        services.Configure(setupAction);
        return services;
    }
}