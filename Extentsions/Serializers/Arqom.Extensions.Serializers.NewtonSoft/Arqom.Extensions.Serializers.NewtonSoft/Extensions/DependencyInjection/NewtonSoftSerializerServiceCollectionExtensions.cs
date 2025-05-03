using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.Serializers.NewtonSoft.Services;
using Arqom.Extensions.Serializers.Abstractions;

namespace Arqom.Extensions.DependencyInjection;

public static class NewtonSoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddArqomNewtonSoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, NewtonSoftSerializer>();
}