using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.Modules;
using Tnf.AspNetCore;
using Tnf.AspNetCore.Configuration;
using Tnf.Architecture.Application;
using Tnf.Architecture.Domain.Configuration;
using Tnf.App.App.Configuration;
using Tnf.Architecture.CrossCutting;

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
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            // Inicializa a localização multiTenant, sendo o faultBack as localiações em arquivo
            Configuration.Modules.TnfApp().LanguageManagement.EnableDbLocalization();
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(AppConsts.ConnectionStringName);

            //Configuration.Modules.TnfAspNetCore()
            //    .CreateControllersForAppServices(
            //        typeof(AppModule).Assembly
            //    );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}