using Arqom.Extensions.Payment.Abstractions;
using Arqom.Extensions.Payments.Mellat.Services;
using Microsoft.Extensions.DependencyInjection;
using ServiceReference;

namespace Arqom.Extensions.Payments.Mellat.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMellatGateway(this IServiceCollection services)
    {
        services.AddTransient<IPaymentGateway, PaymentGatewayClient>();
        services.AddSingleton<IPaymentProvider, MellatPaymentProvider>();

        return services;
    }
}
