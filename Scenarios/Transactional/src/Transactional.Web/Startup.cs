using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Tnf.Configuration;
using Transacional.Domain;
using Transactional.Domain;
using Transactional.Infra;
using Transactional.Web.HostedServices;

namespace Transactional.Web
{
    public class Startup
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public Startup(IConfiguration configuration)
        {
            _databaseConfiguration = new DatabaseConfiguration(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddDomainDependency()              // dependencia da camada Transactional.Domain
                .AddInfraDependency()               // dependencia da camada Transactional.Infra
                .AddTnfAspNetCore(options =>
                {
                    // Adiciona as configurações de localização da aplicação
                    options.ConfigureLocalization();

                    // Configura a connection string da aplicação
                    options.DefaultConnectionString(_databaseConfiguration.ConnectionString);

                    // ---------- Configurações de Unit of Work a nível de aplicação

                    options.UnitOfWorkOptions(unitOfWorkOptions =>
                    {
                        // Por padrão um Uow é transacional: todas as operações realizadas dentro de um Uow serão
                        // comitadas ou desfeitas em caso de erro
                        unitOfWorkOptions.IsTransactional = true;

                        // IsolationLevel default de cada transação criada. (Precisa da configuração IsTransactional = true para funcionar)
                        unitOfWorkOptions.IsolationLevel = IsolationLevel.ReadCommitted;

                        // Escopo da transação. (Precisa da configuração IsTransactional = true para funcionar)
                        unitOfWorkOptions.Scope = TransactionScopeOption.Required;

                        // Timeout que será aplicado (se este valor for informado) para toda nova transação criada
                        // Não é indicado informar este valor pois irá afetar toda a aplicação.
                        unitOfWorkOptions.Timeout = TimeSpan.FromSeconds(5);
                    });


                    // ----------
                });                                 // dependencia do pacote Tnf.AspNetCore

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transactional API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Transactional.Web.xml"));
                });

            services.AddHostedService<MigrationHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseRouting();

            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transactional API v1");
            });

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
