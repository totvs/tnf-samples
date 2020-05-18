using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Querying.Infra.Context;
using Querying.Infra.Dto;
using Querying.Infra.Entities;
using System;
using Tnf.Configuration;

namespace Querying.Web.Tests
{
    public class StartupIntegratedTest
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddInfraDependency()               // Configura a mesma dependencia da camada de infra a ser testada
                .AddTnfAspNetCoreSetupTest()        // Configura o setup de teste para AspNetCore
                .AddTnfEfCoreSqliteInMemory()       // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<PurchaseOrderContext>();    // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest(options =>
            {
                options.Repository(repositoryConfig =>
                {
                    repositoryConfig.Entity<IEntity>(entity => entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
