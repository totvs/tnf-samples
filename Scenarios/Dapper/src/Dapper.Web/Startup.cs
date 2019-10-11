using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dapper.Infra;
using System;
using System.Threading.Tasks;
using Tnf.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Dapper.Infra.Entities;
using Dapper.Infra.Dto;
using System.IO;
using Dapper.Web.HostedServices;

namespace Dapper.Web
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddInfraDependency()       // Adiciona a dependencia da camada de Infra
                .AddTnfAspNetCore();        // Adiciona a dependencia de AspNetCore do Tnf

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Dapper API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Dapper.Web.xml"));
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
                // Recupera a configuração da aplicação
                var configuration = options.Settings.FromJsonFiles(env.ContentRootPath, $"appsettings.{env.EnvironmentName}.json");

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = configuration.GetConnectionString(Constants.ConnectionStringName);

                options.Repository(repositoryConfig =>
                {
                    repositoryConfig.Entity<IEntity>(entity => entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                });
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dapper API v1");
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
