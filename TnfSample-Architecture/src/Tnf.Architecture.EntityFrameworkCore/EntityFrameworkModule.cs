using System.Reflection;
using Tnf.App.EntityFrameworkCore;
using Tnf.Architecture.Domain;
using Tnf.Modules;

namespace Tnf.Architecture.EntityFrameworkCore
{
    [DependsOn(
        typeof(DomainModule),
        typeof(TnfAppEntityFrameworkCoreModule))]
    public class EntityFrameworkModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}