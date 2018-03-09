using HelloWorld.SharedKernel;
using HelloWorld.SharedKernel.External;
using HelloWorld.Web.Tests.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace HelloWorld.Web.Tests
{
    public class StartupTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Configura a mesma dependencia da camada web a ser testada
            services.AddApplicationServiceDependency();

            // Configura o setup de teste para AspNetCore
            services.AddTnfAspNetCoreSetupTest();

            // Sobrescrita da classe concreta pela classe de mock
            services.ReplaceTransient<ILocationService, LocalizationServiceMock>();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest(options =>
            {
                // Adiciona as configurações de localização da aplicação a ser testada
                options.ConfigureSharedKernelLocalization();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
