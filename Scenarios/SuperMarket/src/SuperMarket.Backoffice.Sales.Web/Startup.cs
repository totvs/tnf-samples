using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuperMarket.Backoffice.Sales.Application;
using SuperMarket.Backoffice.Sales.Domain;
using SuperMarket.Backoffice.Sales.Infra;
using SuperMarket.Backoffice.Sales.Infra.Queue;
using SuperMarket.Backoffice.Sales.Web.HostedServices;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.Sales.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly DatabaseConfiguration _databaseConfiguration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _databaseConfiguration = new DatabaseConfiguration(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddTnfMetrics(Configuration)
                .AddSalesApplicationDependency()
                .AddTnfAspNetCore(options =>
                {
                    // Adiciona as configurações de localização da aplicação
                    options.ConfigureSalesDomain();

                    // Configura as dependencias de RequestDto
                    options.ConfigureInfra();

                    // Configura a connection string da aplicação
                    options.DefaultConnectionString(_databaseConfiguration.ConnectionString);

                    // ---------- Configurações de Unit of Work a nível de aplicação

                    // Forçando a estrategia de UnitOfWork a não ser transacional.
                    // Isso quer dizer que irá ser controlado manualmente esse comportamento no código
                    options.UnitOfWorkOptions(unitOfWork => unitOfWork.IsTransactional = false);

                    // ----------

                    options.ConfigureSalesQueueInfraDependency();
                });

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SuperMarket.Backoffice.Sales.Web.xml"));
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales API v1");
            });

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
