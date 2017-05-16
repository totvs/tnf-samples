using Tnf.Modules;
using Tnf.Architecture.Domain;
using Tnf.Architecture.Data;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.Reflection.Extensions;
using Tnf.Castle.Log4Net;
using Castle.Facilities.Logging;
using Tnf.Architecture.Mapper;
using Tnf.App;

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
            IocManager.RegisterAssemblyByConvention(typeof(AppModule).GetAssembly());
        }
    }
}
