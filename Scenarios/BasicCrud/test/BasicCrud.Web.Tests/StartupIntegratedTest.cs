using BasicCrud.Application;
using BasicCrud.Domain;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto;
using BasicCrud.Infra;
using BasicCrud.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tnf.Configuration;

namespace BasicCrud.Web.Tests
{
    public class StartupIntegratedTest
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Configura o mesmo Mapper para ser testado
            services.AddMapperDependency();

            // Configura a mesma dependencia da camada web a ser testada
            services.AddApplicationServiceDependency();

            // Configura o setup de teste para AspNetCore
            services.AddTnfAspNetCoreSetupTest(builder =>
            {
                // Adiciona as configurações de localização da aplicação a ser testada
                builder.UseDomainLocalization();

                // Configuração global de como irá funcionar o Get utilizando o repositorio do Tnf
                // O exemplo abaixo registra esse comportamento através de uma convenção:
                // toda classe que implementar essas interfaces irão ter essa configuração definida
                // quando for consultado um método que receba a interface IRequestDto do Tnf
                builder.Repository(repositoryConfig =>
                {
                    repositoryConfig.Entity<IEntity>(entity =>
                        entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                });

                // Configuração para usar teste com EntityFramework em memória
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.Unspecified;
            });

            // Configura o setup de teste para EntityFrameworkCore em memória
            services.AddTnfEfCoreSqliteInMemory()
                // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore
                .RegisterDbContextToSqliteInMemory<CrudDbContext, FakeCrudDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapDefaultControllerRoute();
            });
        }
    }
}
