using Case1.Domain;
using Tnf.Configuration;
using Tnf.Localization.Dictionaries;

namespace Tnf.Localization
{
    public static class LocalizationExtensions
    {
        public static void AddDomainLocalization(this ITnfConfiguration configuration)
        {
            // Incluindo o source de localização
            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(Case1Consts.LocalizationSourceName,
                new JsonEmbeddedFileLocalizationDictionaryProvider(typeof(Case1Consts).Assembly, "Case1.Domain.Localization.SourceFiles")));

            // Incluindo suporte as seguintes linguagens
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));
            configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));
        }
    }
}
