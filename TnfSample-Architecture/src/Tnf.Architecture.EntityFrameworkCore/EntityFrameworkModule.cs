using Tnf.App.EntityFrameworkCore;
using Tnf.Architecture.Domain;
using Tnf.Dapper;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.EntityFrameworkCore
{
    [DependsOn(
        typeof(DomainModule),
        typeof(TnfDapperModule),
        typeof(TnfAppEntityFrameworkCoreModule))]
    public class EntityFrameworkModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EntityFrameworkModule).GetAssembly());
        }
    }
}