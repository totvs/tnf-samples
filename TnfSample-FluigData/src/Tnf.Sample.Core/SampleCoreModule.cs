using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Sample.Core
{
    public class SampleCoreModule : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SampleCoreModule).GetAssembly());
        }
    }
}
