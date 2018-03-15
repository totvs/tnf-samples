using Tnf.Configuration;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;

namespace BasicCrud.Domain
{
    public static class LocalizationExtensions
    {
        public static void UseDomainLocalization(this ITnfConfiguration configuration)
        {
            // Incluindo o source de localização
            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(DomainConstants.LocalizationSourceName,
                new JsonEmbeddedFileLocalizationDictionaryProvider(
                    typeof(DomainConstants).Assembly, 
                    "BasicCrud.Domain.Localization.SourceFiles")));

            // Incluindo suporte as seguintes linguagens
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));
            configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));
        }
    }
}
