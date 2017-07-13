﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.App.AspNetCore;
using Tnf.App.Security.Identity;
using Tnf.Architecture.Application;
using Tnf.Architecture.Domain.Configuration;
using Tnf.Architecture.Dto;
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
            var enviroment = "Development";
#elif RELEASE
            var enviroment = "Release";
#endif

            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, enviroment);
        }

        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabled = true;
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