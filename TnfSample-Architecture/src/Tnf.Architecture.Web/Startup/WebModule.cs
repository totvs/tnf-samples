using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.App.AspNetCore;
using Tnf.Architecture.Application;
using Tnf.Architecture.Domain.Configuration;
using Tnf.Architecture.Dto;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Web.Startup
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfAppAspNetCoreModule))]
    public class WebModule : TnfModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public WebModule(IHostingEnvironment env)
        {
            var enviroment = env.EnvironmentName;

#if DEBUG
            enviroment = "Development";
#elif RELEASE
            enviroment = "Release";
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