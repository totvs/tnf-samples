using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddDomainDependency()              // dependencia da camada Transactional.Domain
                .AddInfraDependency()               // dependencia da camada Transactional.Infra
                .AddTnfAspNetCore();                // dependencia do pacote Tnf.AspNetCore

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Transactional API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Transactional.Web.xml"));
                });

            services.AddHostedService<MigrationHostedService>();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação
                options.ConfigureLocalization();

                // Recupera a configuração da aplicação
                var configuration = options.Settings.FromJsonFiles(env.ContentRootPath, $"appsettings.{env.EnvironmentName}.json");

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = _databaseConfiguration.ConnectionString;

                // ---------- Configurações de Unit of Work a nível de aplicação

                // Por padrão um Uow é transacional: todas as operações realizadas dentro de um Uow serão
                // comitadas ou desfeitas em caso de erro
                options.UnitOfWorkOptions().IsTransactional = true;

                // IsolationLevel default de cada transação criada. (Precisa da configuração IsTransactional = true para funcionar)
                options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

                // Escopo da transação. (Precisa da configuração IsTransactional = true para funcionar)
                options.UnitOfWorkOptions().Scope = TransactionScopeOption.Required;

                // Timeout que será aplicado (se este valor for informado) para toda nova transação criada
                // Não é indicado informar este valor pois irá afetar toda a aplicação.
                options.UnitOfWorkOptions().Timeout = TimeSpan.FromSeconds(5);

                // ----------
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transactional API v1");
            });

            app.UseMvcWithDefaultRoute();
            app.UseResponseCompression();

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        }
    }
}
