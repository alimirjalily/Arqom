using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.UsersManagement.Options;
using Arqom.Extensions.UsersManagement.Services;
using Arqom.Extensions.UsersManagement.Abstractions;

namespace Arqom.Extensions.DependencyInjection;

public static class UserInfoServiceCollectionExtensions
{
    public static IServiceCollection AddArqomWebUserInfoService(this IServiceCollection services, IConfiguration configuration, bool useFake = false)
    {
        if (useFake)
        {
            services.AddSingleton<IUserInfoService, FakeUserInfoService>();

        }
        else
        {
            services.Configure<UserManagementOptions>(configuration);
            services.AddSingleton<IUserInfoService, WebUserInfoService>();

        }
        return services;
    }


    public static IServiceCollection AddArqomWebUserInfoService(this IServiceCollection services, IConfiguration configuration, string sectionName, bool useFake = false)
    {
        services.AddArqomWebUserInfoService(configuration.GetSection(sectionName), useFake);
        return services;
    }

    public static IServiceCollection AddArqomWebUserInfoService(this IServiceCollection services, Action<UserManagementOptions> setupAction, bool useFake = false)
    {
        if (useFake)
        {
            services.AddSingleton<IUserInfoService, FakeUserInfoService>();

        }
        else
        {
            services.Configure(setupAction);
            services.AddSingleton<IUserInfoService, WebUserInfoService>();

        }
        return services;
    }
}

