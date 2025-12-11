using Microsoft.Extensions.DependencyInjection;
using Tnf.Configuration;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;

namespace ProdutoXyz.Domain
{
    public static class TnfConfigurationExtensions
    {
        public static void UseDomainLocalization(this ITnfBuilder builder)
        {
            builder.Localization(localization =>
            {
                // Incluindo o source de localização
                localization.AddJsonEmbeddedLocalizationFile(
                    Constants.LocalizationSourceName,
                    typeof(Constants).Assembly,
                    "ProdutoXyz.Domain.Localization.SourceFiles");

                // Incluindo suporte as seguintes linguagens
                localization.AddLanguage("pt-BR", "Português", isDefault: true);
                localization.AddLanguage("en", "English");
            });

        }
    }
}
