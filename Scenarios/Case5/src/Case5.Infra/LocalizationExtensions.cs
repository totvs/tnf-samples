using Case5.Infra;
using Tnf.Configuration;
using Tnf.Localization.Dictionaries;

namespace Tnf.Localization
{
    public static class LocalizationExtensions
    {
        public static void AddInfraLocalization(this ITnfConfiguration configuration)
        {
            // Incluindo suporte as seguintes linguagens
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português (Brasil)", isDefault: true));
            configuration.Localization.Languages.Add(new LanguageInfo("en", "Inglês"));

            // Configura qual o source de localização da aplicação
            // Mesmo habilitando a localização via banco de dados é necessário definir um source
            // de localização fisico que irá funcionar como fallback se a key informada não for encontrada
            // no banco de dados
            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    InfraConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(typeof(InfraConsts).Assembly, "Case5.Infra.Localization.JsonSources")));

            // Incluindo o source de localização
            configuration.EnableDbLocalization();
        }
    }
}