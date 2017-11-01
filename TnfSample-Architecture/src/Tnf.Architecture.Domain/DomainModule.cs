using Tnf.App;
using Tnf.Architecture.Common;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;
using Tnf.Localization.Dictionaries.Json;
using Tnf.Modules;

namespace Tnf.Architecture.Domain
{
    [DependsOn(typeof(TnfAppModule))]
    public class DomainModule : TnfModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();

            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", isDefault: true));
            Configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português"));

            // Set the localization file for the solution errors
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(AppConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DomainModule).Assembly,
                        "Tnf.Architecture.Domain.Localization.SourceFiles"
                    )
                )
            );

            // Set the localization file for the Tnf errors
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(TnfAppConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DomainModule).Assembly,
                        "Tnf.Architecture.Domain.Localization.TnfSourceFiles"
                    )
                )
            );
        }

        public override void Initialize()
        {
            base.Initialize();

            // Register all the interfaces and its implmentations on this assembly
            IocManager.RegisterAssemblyByConvention<DomainModule>();
        }
    }
}
