using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Sales.Application.Services.Interfaces;
using SuperMarket.Backoffice.Sales.Web.Tests.Mocks;
using System;

namespace SuperMarket.Backoffice.Sales.Web.Tests
{
    public class StartupControllerTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Configura o setup de teste para AspNetCore
            services
                .AddTnfAspNetCoreSetupTest();

            // Registro dos serviços de Mock
            services.AddTransient<IPurchaseOrderAppService, PurchaseOrderAppServiceMock>();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest(options =>
            {
                // Adiciona as configurações de localização da aplicação a ser testada
                options.ConfigureCrudDomain();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
