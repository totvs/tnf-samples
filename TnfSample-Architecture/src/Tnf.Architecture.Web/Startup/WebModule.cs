using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.App.AspNetCore;
using Tnf.App.Configuration;
using Tnf.App.Security.Identity;
using Tnf.Architecture.Application;
using Tnf.Architecture.Common;
using Tnf.Modules;

namespace Tnf.Architecture.Web.Startup
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfAppSecurityIdentityModule),
        typeof(TnfAppAspNetCoreModule))]
    public class WebModule : TnfModule
    {
        private IHostingEnvironment _env;
        public WebModule(IHostingEnvironment env)
        {
            _env = env;
        }

        public override void PreInitialize()
        {
            var configuration = Configuration
                                    .Settings
                                    .FromJsonFiles(_env.ContentRootPath, $"appsettings.{_env.EnvironmentName}.json");

            Configuration.DefaultNameOrConnectionString = configuration.GetConnectionString(AppConsts.ConnectionStringName);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebModule).Assembly);
        }
    }
}