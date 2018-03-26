using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperMarket.Backoffice.FiscalService.Domain;
using SuperMarket.Backoffice.FiscalService.Infra;
using SuperMarket.Backoffice.FiscalService.Infra.Queue;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.FiscalService.Web
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddFiscalDomainDependency()
                .AddFiscalInfraDependency()
                .AddFiscalInfraQueueDependency()
                .AddTnfAspNetCore();

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação
                options.ConfigureFiscalDomain();

                // Recupera a configuração da aplicação
                var configuration = options.Settings.FromJsonFiles(env.ContentRootPath, $"appsettings.{env.EnvironmentName}.json");

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = configuration.GetConnectionString(Constants.ConnectionStringName);


                // ---------- Configurações de Unit of Work a nível de aplicação

                // Forçando a estrategia de UnitOfWork a não ser transacional.
                // Isso quer dizer que irá ser controlado manualmente esse comportamento no código
                options.UnitOfWorkOptions().IsTransactional = false;

                // ----------

                options.ConfigureFiscalServiceQueueInfraDependency();
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

            app.UseSwagger((httpRequest, swaggerDoc) =>
            {
                swaggerDoc.Host = httpRequest.Host.Value;
            });
            app.UseSwaggerUi(); //URL: /swagger/ui

            app.Run(context =>
            {
                context.Response.Redirect("/swagger/ui");
                return Task.CompletedTask;
            });

            logger.LogInformation("Start application ...");
        }
    }
}
