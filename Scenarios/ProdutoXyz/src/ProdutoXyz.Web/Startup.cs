using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProdutoXyz.Application;
using ProdutoXyz.Domain;
using ProdutoXyz.Domain.Entities;
using ProdutoXyz.Dto;
using ProdutoXyz.Infra;
using ProdutoXyz.Infra.SqLite;
using System;
using System.IO;
using Tnf.Configuration;

namespace ProdutoXyz.Web
{
    public class Startup
    {
        DatabaseConfiguration DatabaseConfiguration { get; }
        RacConfiguration RacConfiguration { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DatabaseConfiguration = new DatabaseConfiguration(configuration);
            RacConfiguration = new RacConfiguration(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddApplicationServiceDependency()
                .AddTnfAspNetCore(options =>
                {
                    // Adiciona as configurações de localização da aplicação
                    options.UseDomainLocalization();

                    // Configuração global de como irá funcionar o Get utilizando o repositorio do Tnf
                    // O exemplo abaixo registra esse comportamento através de uma convenção:
                    // toda classe que implementar essas interfaces irão ter essa configuração definida
                    // quando for consultado um método que receba a interface IRequestDto do Tnf
                    options.Repository(repositoryConfig =>
                    {
                        repositoryConfig.Entity<IEntity>(entity =>
                            entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                    });

                    // Configura a connection string da aplicação
                    options.DefaultConnectionString(DatabaseConfiguration.ConnectionString);

                    // habilita drive Devart para PostgreSQL
                    if (DatabaseConfiguration.DatabaseType == DatabaseType.Postgres)
                        options.EnableDevartPostgreSQLDriver();

                    // Habita o suporte ao multitenancy
                    options.MultiTenancy(tenancy => tenancy.IsEnabled = true);
                })
                .AddTnfAspNetCoreSecurity(Configuration);

            services.AddSingleton(RacConfiguration);

            if (DatabaseConfiguration.DatabaseType == DatabaseType.Sqlite)
                services.AddSqLiteDependency();
            else if (DatabaseConfiguration.DatabaseType == DatabaseType.Postgres)
                services.AddPostgressDependency();
            else
                throw new NotSupportedException("No database configuration found");

            if (DatabaseConfiguration.DatabaseType == DatabaseType.Sqlite)

                services.AddSingleton(RacConfiguration);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddHostedService<MigrationHostedService>();

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Produto XYZ API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ProdutoXyz.Web.xml"));
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                        Type = SecuritySchemeType.ApiKey
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Authorization" }
                            },
                            new string[0]
                        },
                    });
                });

            services.AddSingleton(RacConfiguration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseTnfAspNetCore();

            app.UseRouting();

            app.UseTnfAspNetCoreSecurity();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Produto XYZ v1");
            });

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSpa(spa =>
            {
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                    spa.UseAngularCliServer(npmScript: "start");
            });
        }
    }
}
