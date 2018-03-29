using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Transacional.Domain;
using Transactional.Domain;
using Transactional.Infra.Context;

namespace Transactional.Web.Tests
{
    public class StartupTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {            
            services
                .AddDomainDependency()                                  // dependencia da camada Transactional.Domain
                .AddTnfEfCoreSqliteInMemory()                           // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<PurchaseOrderContext>();     // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore

            services.AddTnfAspNetCoreSetupTest();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest(options =>
            {
                // Adiciona as configurações de localização da aplicação a ser testada
                options.ConfigureLocalization();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
