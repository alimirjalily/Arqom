using Arqom.Extensions.PaymentGateways.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arqom.Extensions.PaymentGateways.Zarinpal;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddZarinpalGatewayAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ZarinpalOptions>(configuration.GetSection("PaymentGateways:Zarinpal"));
        services.AddHttpClient<ZarinpalPaymentAdapter>();
        services.AddTransient<IPaymentGatewayAdapter, ZarinpalPaymentAdapter>();

        return services;
    }
}
