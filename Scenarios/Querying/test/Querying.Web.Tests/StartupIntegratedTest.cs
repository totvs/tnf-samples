using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Querying.Infra.Context;
using System;

namespace Querying.Web.Tests
{
    public class StartupIntegratedTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddInfraDependency()               // Configura a mesma dependencia da camada de infra a ser testada
                .AddTnfAspNetCoreSetupTest()        // Configura o setup de teste para AspNetCore
                .AddTnfEfCoreSqliteInMemory()       // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<PurchaseOrderContext>();    // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest();

            // Habilita o uso do UnitOfWork em todo o request
            app.UseTnfUnitOfWork();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
