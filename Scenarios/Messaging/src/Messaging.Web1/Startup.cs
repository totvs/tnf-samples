using Messaging.Infra1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using Tnf.Bus.Client;
using Tnf.Bus.Queue.RabbitMQ;
using Tnf.Configuration;
using Tnf.Localization;

namespace Messaging.Web1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Adiciona a dependencia de AspNetCore do Tnf
            services
                .AddCorsAll("AllowAll")
                .AddInfra1Dependency()
                .AddTnfAspNetCore(builder =>
                {
                    builder.AddInfraLocalization();

                    // Recupera as configurações da fila
                    var exchangeRouter = QueuConfiguration.GetExchangeRouterConfiguration();

                    // Configura para que ela publique mensagens
                    builder.BusClient(busClient =>
                    {
                        busClient.AddPublisher(
                            exBuilder: e => exchangeRouter,
                            listener: er => new PublisherListener(
                                exchangeRouter: er,
                                serviceProvider: busClient.ServiceProvider));
                    });
                });

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Publish API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Messaging.Web1.xml"));
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publish API v1");
            });

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
