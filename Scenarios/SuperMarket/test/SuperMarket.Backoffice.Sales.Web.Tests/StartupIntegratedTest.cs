using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Sales.Application;
using SuperMarket.Backoffice.Sales.Domain;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using SuperMarket.Backoffice.Sales.Infra;
using SuperMarket.Backoffice.Sales.Infra.Contexts;
using SuperMarket.Backoffice.Sales.Web.Tests.Mocks;
using System;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.Sales.Web.Tests
{
    public class StartupIntegratedTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddSalesApplicationDependency()            // Configura a mesma dependencia da camada web a ser testada   
                .AddTnfAspNetCoreSetupTest()                // Configura o setup de teste para AspNetCore
                .AddTnfEfCoreSqliteInMemory()               // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<SalesContext>(); // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore

            // Registro dos serviços de Mock
            services.AddTransient<IPriceTableRepository, PriceTableRepositoryMock>();
            services.AddSingleton<PriceTableMock>();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest(options =>
            {
                // Adiciona as configurações de localização da aplicação a ser testada
                options.ConfigureSalesDomain();
                options.ConfigureInfra();

                options.UnitOfWorkOptions().IsTransactional = false;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
