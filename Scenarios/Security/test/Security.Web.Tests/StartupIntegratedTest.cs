using Security.Application;
using Security.Domain;
using Security.Domain.Entities;
using Security.Dto;
using Security.Infra;
using Security.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tnf.Configuration;
using System.Security.Claims;
using System.Collections.Generic;
using Tnf.Runtime.Security;
using IdentityServer4.AccessTokenValidation;

namespace Security.Web.Tests
{
    public class StartupIntegratedTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {            
            services
                .AddMapperDependency()                                  // Configura o mesmo Mapper para ser testado
                .AddApplicationServiceDependency()                      // Configura a mesma dependencia da camada web a ser testada   
                .AddTnfAspNetCoreSetupTest()                            // Configura o setup de teste para AspNetCore
                .AddTnfEfCoreSqliteInMemory()                           // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<CrudDbContext, FakeCrudDbContext>();    // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest(options =>
            {
                // Adiciona as configurações de localização da aplicação a ser testada
                options.UseDomainLocalization();

                // Configuração global de como irá funcionar o Get utilizando o repositorio do Tnf
                // O exemplo abaixo registra esse comportamento através de uma convenção:
                // toda classe que implementar essas interfaces irão ter essa configuração definida
                // quando for consultado um método que receba a interface IRequestDto do Tnf
                options.Repository(repositoryConfig =>
                {
                    repositoryConfig.Entity<IEntity>(entity =>
                        entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                });

                // Configuração para usar teste com EntityFramework em memória
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.Unspecified;
            });

            // Habilita o uso do UnitOfWork em todo o request
            app.UseTnfUnitOfWork();

            // Authentication
            app.Use(async (context, next) =>
            {
                var claimsIdentity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(TnfClaimTypes.UserId, "1"),
                    new Claim(TnfClaimTypes.UserName, "User 1")
                }, IdentityServerAuthenticationDefaults.AuthenticationScheme);

                context.User = new ClaimsPrincipal(claimsIdentity);
                await next.Invoke();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
