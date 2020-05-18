using Dapper.Infra.Context;
using Dapper.Infra.Dto;
using Dapper.Infra.Entities;
using Dapper.Infra.Mappers.DapperMappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tnf.Configuration;
using Tnf.Dapper;

namespace Dapper.Web.Tests
{
    public class StartupIntegratedTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddInfraDependency()               // Configura a mesma dependencia da camada de infra a ser testada   
                .AddTnfDapper(options =>
                {
                    options.MapperAssemblies.Add(typeof(CustomerMapper).Assembly);
                    options.DbType = DapperDbType.Sqlite;
                })
                .AddTnfAspNetCoreSetupTest(options =>
                {
                    options.Repository(repositoryConfig =>
                    {
                        repositoryConfig.Entity<IEntity>(entity => entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                    });
                })        // Configura o setup de teste para AspNetCore
                .AddTnfEfCoreSqliteInMemory()       // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<PurchaseOrderContext>();    // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
