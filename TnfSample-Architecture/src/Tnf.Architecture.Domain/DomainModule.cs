using Tnf.App;
using Tnf.Architecture.Common;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;
using Tnf.Localization.Dictionaries.Json;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Domain
{
    [DependsOn(typeof(TnfAppModule))]
    public class DomainModule : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", isDefault: true));
            Configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português"));

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(AppConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DomainModule).GetAssembly(),
                        "Tnf.Architecture.Domain.Localization.SourceFiles"
                    )
                )
            );

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(TnfAppConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DomainModule).GetAssembly(),
                        "Tnf.Architecture.Domain.Localization.TnfSourceFiles"
                    )
                )
            );

            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DomainModule).GetAssembly());
        }
    }
}
