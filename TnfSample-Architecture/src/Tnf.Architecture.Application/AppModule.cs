using Castle.Facilities.Logging;
using Tnf.Architecture.Carol;
using Tnf.Architecture.Domain;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.Architecture.Mapper;
using Tnf.Castle.Log4Net;
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
        public override void PreInitialize()
        {
            base.PreInitialize();

            //Configure Log4Net logging
            IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseTnfLog4Net().WithConfig("log4net.config")
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppModule).Assembly);
        }
    }
}
