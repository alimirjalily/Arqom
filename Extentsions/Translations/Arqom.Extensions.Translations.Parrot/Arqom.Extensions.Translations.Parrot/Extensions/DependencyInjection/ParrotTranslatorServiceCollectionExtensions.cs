using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.Translations.Parrot.Options;
using Arqom.Extensions.Translations.Parrot.Services;
using Arqom.Extensions.Translations.Abstractions;

namespace Arqom.Extensions.DependencyInjection;

public static class ParrotTranslatorServiceCollectionExtensions
{
    public static IServiceCollection AddArqomParrotTranslator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure<ParrotTranslatorOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddArqomParrotTranslator(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddArqomParrotTranslator(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddArqomParrotTranslator(this IServiceCollection services, Action<ParrotTranslatorOptions> setupAction)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure(setupAction);
        return services;
    }
}