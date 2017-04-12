using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.Modules;
using Tnf.AspNetCore;
using Tnf.AspNetCore.Configuration;
using Tnf.Sample.Application;
using Tnf.Sample.Core.Configuration;
using Tnf.Sample.Data.FluigData;

namespace Tnf.Sample.Web.Startup
{
    [DependsOn(
        typeof(SampleAppModule),
        typeof(SampleDataModule),
        typeof(TnfAspNetCoreModule))]
    public class SampleAppWebModule : TnfModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SampleAppWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.Modules.TnfAspNetCore();
                //.CreateControllersForAppServices(
                //    typeof(SampleAppModule).Assembly
                //);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}