using System.Reflection;
using Tnf.Modules;

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
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}