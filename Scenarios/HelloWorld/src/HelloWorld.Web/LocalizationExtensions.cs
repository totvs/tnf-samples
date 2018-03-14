using Tnf.Configuration;
using Tnf.Localization.Dictionaries;
using Tnf.Localization;

namespace HelloWorld.Web
{
    public static class LocalizationExtensions
    {
        public static void ConfigureLocalization(this ITnfConfiguration configuration)
        {
            // Incluindo o source de localização
            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(Constants.LocalizationSourceName,
                new JsonEmbeddedFileLocalizationDictionaryProvider(
                    typeof(Constants).Assembly,
                    "HelloWorld.Web.Localization.SourceFiles")));

            // Incluindo suporte as seguintes linguagens
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));
            configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));
        }
    }
}
