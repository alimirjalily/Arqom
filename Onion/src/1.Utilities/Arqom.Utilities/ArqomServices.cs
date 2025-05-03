using Microsoft.Extensions.Logging;
using Arqom.Extensions.Caching.Abstractions;
using Arqom.Extensions.ObjectMappers.Abstractions;
using Arqom.Extensions.Serializers.Abstractions;
using Arqom.Extensions.Translations.Abstractions;
using Arqom.Extensions.UsersManagement.Abstractions;

namespace Arqom.Utilities;

public class ArqomServices
{
    public readonly ITranslator Translator;
    public readonly ICacheAdapter CacheAdapter;
    public readonly IMapperAdapter MapperFacade;
    public readonly ILoggerFactory LoggerFactory;
    public readonly IJsonSerializer Serializer;
    public readonly IUserInfoService UserInfoService;

    public ArqomServices(ITranslator translator,
            ILoggerFactory loggerFactory,
            IJsonSerializer serializer,
            IUserInfoService userInfoService,
            ICacheAdapter cacheAdapter,
            IMapperAdapter mapperFacade)
    {
        Translator = translator;
        LoggerFactory = loggerFactory;
        Serializer = serializer;
        UserInfoService = userInfoService;
        CacheAdapter = cacheAdapter;
        MapperFacade = mapperFacade;
    }
}