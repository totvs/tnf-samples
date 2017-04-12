using System.Reflection;
using Tnf.Sample.Configuration;
using Tnf.Sample.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.Modules;
using Tnf.AspNetCore;
using Tnf.AspNetCore.Configuration;
using Tnf.App.App.Configuration;
using Tnf.App;

namespace Tnf.Sample.Web.Startup
{
    [DependsOn(
        typeof(SampleApplicationModule), 
        typeof(SampleEntityFrameworkCoreModule), 
        typeof(TnfAspNetCoreModule))]        
    public class SampleWebModule : TnfModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SampleWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            // Inicializa a localização multiTenant, sendo o faultBack as localiações em arquivo
            Configuration.Modules.TnfApp().LanguageManagement.EnableDbLocalization();

            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(SampleAppConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<SampleAppNavigationProvider>();

            Configuration.Modules.TnfAspNetCore()
                .CreateControllersForAppServices(
                    typeof(SampleApplicationModule).Assembly
                );

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

        }
    }
}