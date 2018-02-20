using Case5.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Tnf.Configuration;
using Tnf.Localization;
using System.Transactions;

namespace Case5.Web
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddTnfAspNetCore()       // Dependência de AspNetCore do Tnf
                .AddInfraDependency()     // Dependência da camada de Infra: Case2.Infra
                .AddSwaggerGen();         // Dependência Swagger

            services.AddCors(options =>
                options
                    .AddPolicy(
                        name: "AllowAll",
                        configurePolicy: builder =>
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials()));

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação localizadas na camada de Infra
                options.AddInfraLocalization();

                // Recupera a configuração da aplicação
                var configuration = options
                    .Settings
                    .FromJsonFiles(
                        basePath: env.ContentRootPath,
                        jsons: $"appsettings.json");

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = configuration.GetConnectionString(name: InfraConsts.ConnectionStringName);

                // Altera o default isolation level para ReadCommitted (ReadUnCommited not supported by Devart)
                options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

                // Habilita o driver Oracle da Devart (DotConnect for Oracle)
                options.EnableDevartOracleDriver(useDefaultLicense: true);
            });

            // Adiciona middleware de Unit of Work
            app.UseTnfUnitOfWork();

            // Exibe exceptions na tela
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add CORS middleware before MVC
            app.UseCors("AllowAll");

            // Add MVC middleware
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            // Setup Swagger
            app
                .UseSwagger((httpRequest, swaggerDoc) =>
                {
                    swaggerDoc.Host = httpRequest.Host.Value;
                })
                .UseSwaggerUi();

            // Pipeline continue
            app.Run(context =>
            {
                context.Response.Redirect("swagger/ui");
                return Task.CompletedTask;
            });
        }
    }
}