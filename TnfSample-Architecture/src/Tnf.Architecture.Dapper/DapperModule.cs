using Tnf.Architecture.Domain;
using Tnf.Dapper;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Dapper
{
    [DependsOn(
        typeof(DomainModule),
        typeof(TnfDapperModule))]
    public class DapperModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DapperModule).GetAssembly());
        }
    }
}
