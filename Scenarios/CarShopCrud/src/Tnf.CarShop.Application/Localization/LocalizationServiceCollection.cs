using Microsoft.Extensions.DependencyInjection;

namespace Tnf.CarShop.Application.Localization;

public static class LocalizationServiceCollection
{
    public static IServiceCollection ConfigureCarShopLocalization(this IServiceCollection services)
    {
        services.ConfigureTnfLocalization(localization =>
        {
            localization.AddJsonEmbeddedLocalizationFile(LocalizationSource.Default, typeof(LocalizationSource).Assembly, LocalizationSource.Namespace);

            localization.AddLanguage(LocalizationSource.DefaultLanguage, isDefault: true);
        });

        return services;
    }
}
