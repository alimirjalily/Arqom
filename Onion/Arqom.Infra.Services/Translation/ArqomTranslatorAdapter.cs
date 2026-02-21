using Arqom.Core.Domain.Services;
using Arqom.Extensions.Translations.Abstractions;
using Arqom.Extensions.DependencyInjection.Abstractions;
using System.Globalization;

namespace Arqom.Infra.Services.Translation;

public class ArqomTranslatorAdapter : ICoreTranslateService , IScopeLifetime
{
    private readonly ITranslator _arqomTranslator;

    public ArqomTranslatorAdapter(ITranslator arqomTranslator)
    {
        _arqomTranslator = arqomTranslator;
    }

    public string GetTranslation(CultureInfo culture, string key)
    {
        return _arqomTranslator[culture, key];
    }

    public void RegisterTranslation(CultureInfo culture, string key, string value)
    {
        _arqomTranslator[culture, key] = value;
    }
}
