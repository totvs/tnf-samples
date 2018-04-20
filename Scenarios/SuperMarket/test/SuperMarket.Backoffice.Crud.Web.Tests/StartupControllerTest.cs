using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Repositories.Interfaces;
using SuperMarket.Backoffice.Crud.Web.Tests.Mocks;
using System;
using Tnf.Domain.Services;

namespace SuperMarket.Backoffice.Crud.Web.Tests
{
    public class StartupControllerTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Configura o setup de teste para AspNetCore
            services
                .AddTnfRepository()
                .AddTnfAspNetCoreSetupTest();

            // Registro dos serviços de Mock
            services.AddTransient<IDomainService<Customer>, CustomerDomainServiceMock>();
            services.AddTransient<IDomainService<Product>, ProductDomainServiceMock>();
            services.AddTransient<IPriceTableRepository, PriceTableRepositoryMock>();

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
