using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.App.App.Configuration;
using Tnf.App.AspNetCore;
using Tnf.Modules;
using Tnf.Sample.Configuration;
using Tnf.Sample.EntityFrameworkCore;

namespace Tnf.Sample.Web.Startup
{
    [DependsOn(
        typeof(SampleApplicationModule), 
        typeof(SampleEntityFrameworkCoreModule), 
        typeof(TnfAppAspNetCoreModule))]        
    public class SampleWebModule : TnfModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SampleWebModule(IHostingEnvironment env)
        {
            AppSettingFileProvider.BasePath = env.ContentRootPath;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            // Inicializa a localização multiTenant, sendo o faultBack as localiações em arquivo
            Configuration.Modules.TnfApp().LanguageManagement.EnableDbLocalization();

            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(SampleAppConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<SampleAppNavigationProvider>();
            
            Configuration.Settings.Providers.Add<AppSettingsDbProvider>();
            Configuration.Settings.Providers.Add<AppSettingFileProvider>();

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

        }
    }
}