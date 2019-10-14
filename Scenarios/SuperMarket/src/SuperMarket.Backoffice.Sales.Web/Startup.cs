using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly DatabaseConfiguration _databaseConfiguration;

        public Startup(IConfiguration configuration)
        {
            _databaseConfiguration = new DatabaseConfiguration(configuration);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddSalesApplicationDependency()
                .AddTnfAspNetCore();

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Sales API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SuperMarket.Backoffice.Sales.Web.xml"));
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
                options.ConfigureSalesDomain();

                // Configura as dependencias de RequestDto
                options.ConfigureInfra();

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = _databaseConfiguration.ConnectionString;

                // ---------- Configurações de Unit of Work a nível de aplicação

                // Forçando a estrategia de UnitOfWork a não ser transacional.
                // Isso quer dizer que irá ser controlado manualmente esse comportamento no código
                options.UnitOfWorkOptions().IsTransactional = false;

                // ----------

                options.ConfigureSalesQueueInfraDependency();
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales API v1");
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
