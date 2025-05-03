using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.Serializers.Abstractions;
using Arqom.Extensions.Serializers.Microsoft.Services;

namespace Arqom.Extensions.DependencyInjection;

public static class MicrosoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddArqomMicrosoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, MicrosoftSerializer>();
}
