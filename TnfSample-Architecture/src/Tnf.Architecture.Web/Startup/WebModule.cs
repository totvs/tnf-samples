using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.App.AspNetCore;
using Tnf.App.Security.Identity;
using Tnf.Architecture.Application;
using Tnf.Architecture.Common;
using Tnf.Architecture.Domain.Configuration;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Web.Startup
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfAppSecurityIdentityModule),
        typeof(TnfAppAspNetCoreModule))]
    public class WebModule : TnfModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public WebModule(IHostingEnvironment env)
        {
#if DEBUG
            const string enviroment = "Development";
#elif RELEASE
            const string enviroment = "Release";
#endif

            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, enviroment);
        }

        public override void PreInitialize()
        {
            // Inicializa a localização multiTenant, sendo o faultBack as localiações em arquivo
            //Configuration.Modules.TnfApp().LanguageManagement.EnableDbLocalization();
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(AppConsts.ConnectionStringName);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebModule).GetAssembly());
        }
    }
}