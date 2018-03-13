using BasicCrud.Application;
using BasicCrud.Domain;
using BasicCrud.Infra;
using BasicCrud.Infra.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Transactions;
using Tnf.Configuration;

namespace BasicCrud.Web.Tests
{
    public class StartupTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {            
            services
                .AddMapperDependency()
                .AddApplicationServiceDependency()  // Configura a mesma dependencia da camada web a ser testada   
                .AddTnfAspNetCoreSetupTest()        // Configura o setup de teste para AspNetCore
                .AddTnfEfCoreInMemory()             // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextInMemory<CustomerDbContext>();    // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest(options =>
            {
                // Adiciona as configurações de localização da aplicação a ser testada
                options.AddDomainLocalization();

                // Configuração para usar teste com EntityFramework em memória
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.Unspecified;
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
