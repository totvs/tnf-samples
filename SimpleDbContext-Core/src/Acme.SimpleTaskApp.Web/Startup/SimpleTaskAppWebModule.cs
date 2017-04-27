using System.Reflection;
using Acme.SimpleTaskApp.Configuration;
using Acme.SimpleTaskApp.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.Modules;
using Tnf.AspNetCore;
using Tnf.AspNetCore.Configuration;
using Tnf.Reflection.Extensions;

namespace Acme.SimpleTaskApp.Web.Startup
{
    [DependsOn(
        typeof(SimpleTaskAppApplicationModule), 
        typeof(SimpleTaskAppEntityFrameworkCoreModule), 
        typeof(TnfAspNetCoreModule))]
    public class SimpleTaskAppWebModule : TnfModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SimpleTaskAppWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(SimpleTaskAppConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<SimpleTaskAppNavigationProvider>();

            Configuration.Modules.TnfAspNetCore()
                .CreateControllersForAppServices(
                    typeof(SimpleTaskAppApplicationModule).GetAssembly()
                );

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleTaskAppWebModule).GetAssembly());

        }
    }
}