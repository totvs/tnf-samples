using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Acme.SimpleTaskApp
{
    public class SimpleTaskAppCoreModule : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleTaskAppCoreModule).GetAssembly());
        }
    }
}