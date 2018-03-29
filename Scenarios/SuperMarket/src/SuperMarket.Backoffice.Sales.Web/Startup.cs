using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperMarket.Backoffice.Sales.Application;
using SuperMarket.Backoffice.Sales.Domain;
using SuperMarket.Backoffice.Sales.Infra;
using SuperMarket.Backoffice.Sales.Infra.Queue;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.Sales.Web
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddSalesApplicationDependency()
                .AddTnfAspNetCore();

            services.AddCorsAll("AllowAll");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Sales API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SuperMarket.Backoffice.Sales.Web.xml"));
            });

            services.AddResponseCompression();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação
                options.ConfigureSalesDomain();

                // Recupera a configuração da aplicação
                var configuration = options.Settings.FromJsonFiles(env.ContentRootPath, $"appsettings.{env.EnvironmentName}.json");

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = configuration.GetConnectionString(Constants.ConnectionStringName);


                // ---------- Configurações de Unit of Work a nível de aplicação

                // Forçando a estrategia de UnitOfWork a não ser transacional.
                // Isso quer dizer que irá ser controlado manualmente esse comportamento no código
                options.UnitOfWorkOptions().IsTransactional = false;

                // ----------

                options.ConfigureSalesQueueInfraDependency();
            });

            logger.LogInformation("Running migrations ...");

            app.ApplicationServices.MigrateDatabase();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Add CORS middleware before MVC
            app.UseCors("AllowAll");

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales API v1");
            });

            app.UseResponseCompression();

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            logger.LogInformation("Start application ...");
        }
    }
}
