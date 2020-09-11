using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SuperMarket.FiscalService.Domain;
using SuperMarket.FiscalService.Infra;
using SuperMarket.FiscalService.Infra.Queue;
using SuperMarket.FiscalService.Web.HostedServices;
using Swashbuckle.AspNetCore.Swagger;
using Tnf.Configuration;

namespace SuperMarket.FiscalService.Web
{
    public class Startup
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddTnfMetrics(Configuration)
                .AddFiscalDomainDependency()
                .AddFiscalInfraDependency()
                .AddFiscalInfraQueueDependency()
                .AddTnfAspNetCore(options =>
                {
                    // Adiciona as configurações de localização da aplicação
                    options.ConfigureFiscalDomain();

                    // Configura a connection string da aplicação
                    options.DefaultConnectionString(_databaseConfiguration.ConnectionString);

                    // ---------- Configurações de Unit of Work a nível de aplicação

                    // Forçando a estrategia de UnitOfWork a não ser transacional.
                    // Isso quer dizer que irá ser controlado manualmente esse comportamento no código
                    options.UnitOfWorkOptions(options => options.IsTransactional = false);

                    // ----------

                    options.ConfigureFiscalServiceQueueInfraDependency();
                });

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fiscal Service API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SuperMarket.FiscalService.Web.xml"));
                });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddHostedService<MigrationHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fiscal Service API v1");
            });

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
