using System.Reflection;
using Tnf.Modules;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;
using Tnf.Localization.Dictionaries.Json;

namespace Tnf.Sample
{
    public class SampleCoreModule : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flags england", isDefault: true));
            Configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", "famfamfam-flags br"));

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(SampleAppConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Tnf.Sample.Core.Localization.SourceFiles"
                    )
                )
            );

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}