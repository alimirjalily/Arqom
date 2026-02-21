using Arqom.Extensions.Payment.Abstractions;
using Arqom.Extensions.Payments.Zarinpal.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Arqom.Extensions.Payments.Zarinpal.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddZarinpalGateway(this IServiceCollection services)
    {
        services.AddHttpClient<ZarinpalPaymentProvider>();
        services.AddSingleton<IPaymentProvider, ZarinpalPaymentProvider>();

        return services;

    }
          

}
