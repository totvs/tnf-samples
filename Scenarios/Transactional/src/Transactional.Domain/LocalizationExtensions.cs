using Microsoft.Extensions.DependencyInjection;
using Tnf.Configuration;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;

namespace Transactional.Domain
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
                    "Transactional.Domain.Localization.SourceFiles");

                // Incluindo suporte as seguintes linguagens
                localization.AddLanguage("pt-BR", "Português", isDefault: true);
                localization.AddLanguage("en", "English");
            });
        }
    }
}
