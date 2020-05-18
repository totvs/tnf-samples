using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using Tnf.Configuration;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;

namespace SuperMarket.Backoffice.Crud.Domain
{
    public static class LocalizationExtensions
    {
        public static ITnfBuilder ConfigureCrudDomain(this ITnfBuilder builder)
        {
            builder.Localization(localization =>
            {
                // Incluindo o source de localização
                localization.AddJsonEmbeddedLocalizationFile(
                    Constants.LocalizationSourceName,
                    typeof(Constants).Assembly,
                    "SuperMarket.Backoffice.Crud.Domain.Localization.SourceFiles");

                // Incluindo suporte as seguintes linguagens
                localization.AddLanguage("pt-BR", "Português", isDefault: true);
                localization.AddLanguage("en", "English");

                builder.Repository(repository =>
                {
                    repository.Entity<IEntity>(entity => entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                });
            });

            return builder;
        }
    }
}
