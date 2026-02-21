using Arqom.Utilities;

namespace Arqom.Extensions.DependencyInjection;
public static class AddArqomServicesExtensions
{
    public static IServiceCollection AddArqomUtilityServices(
        this IServiceCollection services)
    {
        services.AddTransient<ArqomServices>();
        return services;
    }
}