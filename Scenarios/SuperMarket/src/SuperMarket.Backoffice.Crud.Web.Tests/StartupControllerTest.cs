using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Domain.Entities;
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
            services.AddTnfAspNetCoreSetupTest();

            // Registro dos serviços de Mock
            services.AddTransient<IDomainService<Customer, Guid>, CustomerDomainServiceMock>();
            services.AddTransient<IDomainService<Product, Guid>, ProductDomainServiceMock>();

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

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
