using Case4.Domain;
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
                new DictionaryBasedLocalizationSource(Case4Consts.LocalizationSourceName,
                new JsonEmbeddedFileLocalizationDictionaryProvider(typeof(Case4Consts).Assembly, "Case4.Domain.Localization.SourceFiles")));

            // Incluindo suporte as seguintes linguagens
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));
            configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));
        }
    }
}
