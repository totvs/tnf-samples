using Messaging.Infra2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using Tnf.Bus.Queue.RabbitMQ;

namespace Messaging.Web2
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Adiciona a dependencia de AspNetCore do Tnf
            services.AddTnfAspNetCore();

            // Adiciona a dependencia da camada de Infra: Case1.Infra
            services.AddInfra2Dependency();

            services.AddCorsAll("AllowAll");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Message Store API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Messaging.Web2.xml"));
            });

            services.AddResponseCompression();

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
                            serviceProvider: app.ApplicationServices), 
                        poolSize: 2);
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Message Store API v1");
            });

            app.UseResponseCompression();

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        }
    }
}
