using Castle.Facilities.Logging;
using Tnf.App.Castle.Log4Net;
using Tnf.Architecture.Carol;
using Tnf.Architecture.Domain;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.Architecture.Mapper;
using Tnf.Modules;

namespace Tnf.Architecture.Application
{
    [DependsOn(
        typeof(MapperModule),
        typeof(DomainModule),
        typeof(CarolModule),
        typeof(EntityFrameworkModule))]
    public class AppModule : TnfModule
    {        
        public override void Initialize()
        {
            base.Initialize();

            // Register all the interfaces and its implmentations on this assembly
            IocManager.RegisterAssemblyByConvention<AppModule>();
        }
    }
}
