using Arqom.Utilities;

namespace Arqom.Extensions.DependencyInjection;
public static class AddArqomServicesExtensions
{
    public static IServiceCollection AddArqomUntilityServices(
        this IServiceCollection services)
    {
        services.AddTransient<ArqomServices>();
        return services;
    }
}