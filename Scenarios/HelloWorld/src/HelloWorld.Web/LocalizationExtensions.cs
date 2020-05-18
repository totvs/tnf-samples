using Tnf.Configuration;
using Tnf.Localization.Dictionaries;
using Tnf.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWorld.Web
{
    public static class LocalizationExtensions
    {
        public static void ConfigureLocalization(this ITnfBuilder builder)
        {
            builder.Localization(localization =>
            {
                // Incluindo o source de localização
                localization.AddJsonEmbeddedLocalizationFile(
                    Constants.LocalizationSourceName,
                    typeof(Constants).Assembly,
                    "HelloWorld.Web.Localization.SourceFiles");

                // Incluindo suporte as seguintes linguagens
                localization.AddLanguage("pt-BR", "Português", isDefault: true);
                localization.AddLanguage("en", "English");
            });
        }
    }
}
