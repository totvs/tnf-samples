using Tnf.Configuration;

namespace Tnf.Localization
{
    public static class LocalizationExtensions
    {
        public static void AddInfraLocalization(this ITnfConfiguration configuration)
        {
            // Incluindo suporte as seguintes linguagens
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));
            configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));

            // Incluindo o source de localização
            configuration.EnableDbLocalization();
        }
    }
}
