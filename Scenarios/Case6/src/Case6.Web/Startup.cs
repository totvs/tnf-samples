using Case6.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Tnf.Configuration;

namespace Case6.Web
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddTnfAspNetCore()
                .AddInfra6Dependency()
                .AddTnfRepository()
                .AddSwaggerGen();

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
                // Recupera a configuração da aplicação
                var configuration = options
                    .Settings
                    .FromJsonFiles(
                        basePath: env.ContentRootPath,
                        jsons: $"appsettings.json");

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = configuration.GetConnectionString(name: InfraConsts.ConnectionStringName);

                // Habilita o driver Oracle da Devart (DotConnect for Oracle)
                options.EnableDevartOracleDriver(useDefaultLicense: true);
            });

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
            app.UseSwagger((httpRequest, swaggerDoc) =>
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