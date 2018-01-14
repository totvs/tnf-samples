using Case3.Queue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tnf.Bus.Queue.RabbitMQ;

namespace Case2.Web
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Adiciona a dependencia de AspNetCore do Tnf
            services.AddTnfAspNetCore();

            // Adiciona a dependencia da camada de Infra: Case1.Infra
            services.AddInfra2Dependency();

            // Adiciona a dependencia de fila do Tnf
            services.AddQueueDependency();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSwaggerGen();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Recupera as configurações da fila
                var exchangeRouter = QueuConfiguration.GetExchangeRouterConfiguration();

                // Configura para que ela receba mensagens
                options.BusClient()
                   .AddSubscriber(
                        exBuilder: e => exchangeRouter,
                        listener: er => new SubscriberListener(
                            exchangeRouter: er,
                            serviceProvider: app.ApplicationServices))
                            .Run();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add CORS middleware before MVC
            app.UseCors("AllowAll");

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger((httpRequest, swaggerDoc) =>
            {
                swaggerDoc.Host = httpRequest.Host.Value;
            });
            app.UseSwaggerUi(); //URL: /swagger/ui
        }
    }
}
