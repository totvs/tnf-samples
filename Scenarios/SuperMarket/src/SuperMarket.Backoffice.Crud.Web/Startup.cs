using System;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Infra;
using SuperMarket.Backoffice.Crud.Web.HostedServices;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.Crud.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly DatabaseConfiguration _databaseConfiguration;
        private readonly RedisConfiguration _redisConfiguration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            _redisConfiguration = new RedisConfiguration(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddTnfMetrics(Configuration)
                .AddCrudInfraDependency(_redisConfiguration)
                .AddCrudDomainDependency()
                .AddTnfAspNetCore(options =>
                {
                    // Adiciona as configurações de localização da aplicação
                    options.ConfigureCrudDomain();

                    // Configura a connection string da aplicação
                    options.DefaultConnectionString(_databaseConfiguration.ConnectionString);

                    // ---------- Configurações de Unit of Work a nível de aplicação

                    options.UnitOfWorkOptions(unitOfWork =>
                    {
                        // Por padrão um Uow é transacional: todas as operações realizadas dentro de um Uow serão
                        // comitadas ou desfeitas em caso de erro
                        unitOfWork.IsTransactional = true;
                        // IsolationLevel default de cada transação criada. (Precisa da configuração IsTransactional = true para funcionar)
                        unitOfWork.IsolationLevel = IsolationLevel.ReadCommitted;
                        // Escopo da transação. (Precisa da configuração IsTransactional = true para funcionar)
                        unitOfWork.Scope = TransactionScopeOption.Required;
                    });

                    // ----------
                });

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crud API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SuperMarket.Backoffice.Crud.Web.xml"));
                });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddHostedService<MigrationHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseRouting();

            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore();

            app.UseTnfMetrics();

            app.UseTnfHealthChecks();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud API v1");
            });

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
