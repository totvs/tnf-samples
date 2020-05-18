using Microsoft.Extensions.DependencyInjection;
using Tnf.Configuration;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;

namespace SuperMarket.Backoffice.Sales.Domain
{
    public static class LocalizationExtensions
    {
        public static ITnfBuilder ConfigureSalesDomain(this ITnfBuilder builder)
        {
            builder.Localization(localization =>
            {
                // Incluindo o source de localização
                localization.AddJsonEmbeddedLocalizationFile(
                    Constants.LocalizationSourceName,
                    typeof(Constants).Assembly,
                    "SuperMarket.Backoffice.Sales.Domain.Localization.SourceFiles");

                // Incluindo suporte as seguintes linguagens
                localization.AddLanguage("pt-BR", "Português", isDefault: true);
                localization.AddLanguage("en", "English");
            });

            return builder;
        }
    }
}
