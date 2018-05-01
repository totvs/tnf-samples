using Messaging.Infra1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using Tnf.Bus.Queue.RabbitMQ;
using Tnf.Localization;

namespace Messaging.Web1
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Adiciona a dependencia de AspNetCore do Tnf
            services
                .AddCorsAll("AllowAll")
                .AddInfra1Dependency()
                .AddTnfAspNetCore();

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Publish API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Messaging.Web1.xml"));
                });

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                options.AddInfraLocalization();

                // Recupera as configurações da fila
                var exchangeRouter = QueuConfiguration.GetExchangeRouterConfiguration();

                // Configura para que ela publique mensagens
                options.BusClient()
                   .AddPublisher(
                        exBuilder: e => exchangeRouter,
                        listener: er => new PublisherListener(
                            exchangeRouter: er,
                            serviceProvider: app.ApplicationServices));
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publish API v1");
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
