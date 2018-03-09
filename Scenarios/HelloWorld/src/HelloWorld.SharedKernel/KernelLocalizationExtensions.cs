using Tnf.Configuration;
using Tnf.Localization.Dictionaries;
using Tnf.Localization;

namespace HelloWorld.SharedKernel
{
    public static class KernelLocalizationExtensions
    {
        public static void ConfigureSharedKernelLocalization(this ITnfConfiguration configuration)
        {
            // Incluindo o source de localização
            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(Constants.LocalizationSourceName,
                new JsonEmbeddedFileLocalizationDictionaryProvider(
                    typeof(Constants).Assembly,
                    "HelloWorld.SharedKernel.Localization.SourceFiles")));

            // Incluindo suporte as seguintes linguagens
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));
            configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));
        }
    }
}
