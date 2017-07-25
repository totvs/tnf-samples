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
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flags england", isDefault: true));
            Configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", "famfamfam-flags br"));

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(AppConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DomainModule).GetAssembly(),
                        "Tnf.Architecture.Domain.Localization.SourceFiles"
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
