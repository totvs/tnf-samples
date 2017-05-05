using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.Modules;
using Tnf.AspNetCore;
using Tnf.Architecture.Application;
using Tnf.Architecture.Domain.Configuration;
using Tnf.App.Configuration;
using Tnf.Reflection.Extensions;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Web.Startup
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfAspNetCoreModule))]
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
            Configuration.Modules.TnfApp().LanguageManagement.EnableDbLocalization();
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(AppConsts.ConnectionStringName);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebModule).GetAssembly());
        }
    }
}