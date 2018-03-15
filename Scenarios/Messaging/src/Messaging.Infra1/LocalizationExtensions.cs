using Messaging.Infra1;
using Tnf.Configuration;
using Tnf.Localization.Dictionaries;

namespace Tnf.Localization
{
    public static class LocalizationExtensions
    {
        public static void AddInfraLocalization(this ITnfConfiguration configuration)
        {
            // Incluindo o source de localização
            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(Constants.LocalizationSourceName,
                new JsonEmbeddedFileLocalizationDictionaryProvider(typeof(Constants).Assembly, "Messaging.Infra1.Localization.SourceFiles")));

            // Incluindo suporte as seguintes linguagens
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));
            configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));
        }
    }
}
