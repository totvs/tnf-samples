using System.Reflection;
using Tnf.Modules;
using Tnf.Architecture.Domain;
using Tnf.Architecture.Data;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.AutoMapper;

namespace Tnf.Architecture.Application
{
    [DependsOn(
        typeof(TnfAutoMapperModule),
        typeof(DomainModule),
        typeof(FluigDataModule),
        typeof(EntityFrameworkModule))]
    public class AppModule : TnfModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
