using Tnf.Configuration;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;

namespace SuperMarket.Backoffice.FiscalService.Domain
{
    public static class LocalizationExtensions
    {
        public static ITnfConfiguration ConfigureFiscalDomain(this ITnfConfiguration configuration)
        {
            // Incluindo o source de localização
            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(Constants.LocalizationSourceName,
                new JsonEmbeddedFileLocalizationDictionaryProvider(
                    typeof(Constants).Assembly,
                    "SuperMarket.Backoffice.FiscalService.Domain.Localization.SourceFiles")));

            // Incluindo suporte as seguintes linguagens
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));
            configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));

            return configuration;
        }
    }
}
