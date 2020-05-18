using Messaging.Infra2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using Tnf.Bus.Client;
using Tnf.Bus.Queue.RabbitMQ;
using Tnf.Configuration;

namespace Messaging.Web2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddInfra2Dependency()      // Adiciona a dependencia da camada de Infra: Case1.Infra
                .AddTnfAspNetCore(options =>
                {
                    // Recupera as configurações da fila
                    var exchangeRouter = QueuConfiguration.GetExchangeRouterConfiguration();

                    // Configura para que ela receba mensagens
                    options.BusClient(busClient =>
                    {
                        busClient.AddSubscriber(
                            exBuilder: e => exchangeRouter,
                            listener: er => new SubscriberListener(
                                exchangeRouter: er,
                                serviceProvider: busClient.ServiceProvider),
                            poolSize: 2);
                    });
                });

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Message Store API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Messaging.Web2.xml"));
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Message Store API v1");
            });

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
