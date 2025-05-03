using Microsoft.Extensions.DependencyInjection;
using Arqom.Extensions.Serializers.EPPlus.Services;
using Arqom.Extensions.Serializers.Abstractions;

namespace Arqom.Extensions.DependencyInjection;

public static class EPPlusExcelSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddEPPlusExcelSerializer(this IServiceCollection services)
        => services.AddSingleton<IExcelSerializer, EPPlusExcelSerializer>();
}