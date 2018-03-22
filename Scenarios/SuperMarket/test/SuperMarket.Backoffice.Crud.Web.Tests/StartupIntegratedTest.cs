using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra;
using SuperMarket.Backoffice.Crud.Infra.Contexts;
using SuperMarket.Backoffice.Crud.Infra.Repositories;
using SuperMarket.Backoffice.Crud.Infra.Repositories.Interfaces;
using System;
using Tnf.Repositories;

namespace SuperMarket.Backoffice.Crud.Web.Tests
{
    public class StartupIntegratedTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddTnfMemoryCache()                // Configura Cache na memória
                .AddMapperDependency()              // Configura o mesmo Mapper para ser testado
                .AddCrudDomainDependency()          // Configura a mesma dependencia da camada de dominio a ser testada   
                .AddTnfAspNetCoreSetupTest()        // Configura o setup de teste para AspNetCore
                .AddTnfEfCoreSqliteInMemory()       // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<CrudContext>();    // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore

            services.AddTransient<IPriceTableRepository, ProductRepository>();
            services.AddTransient<IRepository<Product, Guid>, ProductRepository>();

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

            // Habilita o uso do UnitOfWork em todo o request
            app.UseTnfUnitOfWork();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
