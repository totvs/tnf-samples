using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Infra;
using SuperMarket.Backoffice.Crud.Infra.Contexts;
using System;

namespace SuperMarket.Backoffice.Crud.Web.Tests
{
    public class StartupIntegratedTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMapperDependency()              // Configura o mesmo Mapper para ser testado
                .AddCrudDomainDependency()          // Configura a mesma dependencia da camada de dominio a ser testada   
                .AddTnfAspNetCoreSetupTest()        // Configura o setup de teste para AspNetCore
                .AddTnfEfCoreSqliteInMemory()       // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<CrudContext>();    // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest(options =>
            {
                // Adiciona as configurações de localização da aplicação a ser testada
                options.ConfigureCrudDomain();
            });

            // Habilita o uso do UnitOfWork em todo o request
            app.UseTnfUnitOfWork();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
