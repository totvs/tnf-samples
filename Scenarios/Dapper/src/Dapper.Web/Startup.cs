using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dapper.Infra;
using System;
using Tnf.Configuration;
using Dapper.Infra.Entities;
using Dapper.Infra.Dto;
using System.IO;
using Dapper.Web.HostedServices;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Dapper.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddInfraDependency();       // Adiciona a dependência da camada de Infra

            // Adiciona e configura as dependências de AspNetCore do Tnf
            services.AddTnfAspNetCore(options =>
            {
                // Configura a connection string da aplicação
                options.DefaultConnectionString(Configuration.GetConnectionString(Constants.ConnectionStringName));

                options.Repository(repositoryConfig =>
                {
                    repositoryConfig.Entity<IEntity>(entity => entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                });
            });

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dapper API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Dapper.Web.xml"));
                });

            services.AddHostedService<MigrationHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseRouting();

            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dapper API v1");
            });

            app.UseResponseCompression();

            app.UseEndpoints(builder =>
            {
                builder.MapDefaultControllerRoute();
            });
        }
    }
}
