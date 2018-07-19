using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutoXyz.Application;
using ProdutoXyz.Domain;
using ProdutoXyz.Domain.Entities;
using ProdutoXyz.Dto;
using ProdutoXyz.Infra;
using ProdutoXyz.Infra.SqLite;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tnf.Configuration;

namespace ProdutoXyz.Web
{
    public class Startup
    {
        DatabaseConfiguration DatabaseConfiguration { get; }
        RacConfiguration RacConfiguration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            DatabaseConfiguration = new DatabaseConfiguration(configuration);
            RacConfiguration = env.LoadRacConfiguration($"racsettings.{env.EnvironmentName}.json");
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddApplicationServiceDependency()
                .AddTnfAspNetCoreSecurity(RacConfiguration);

            if (DatabaseConfiguration.DatabaseType == DatabaseType.Sqlite)
                services.AddSqLiteDependency();
            else
                throw new NotSupportedException("No database configuration found");

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Produto XYZ API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ProdutoXyz.Web.xml"));
                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        Name = "Authorization",
                        In = "header",
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                        Type = "apiKey"
                    });
                    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                        { "Bearer", Enumerable.Empty<string>() },
                    });
                });

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseTnfAspNetCore(options =>
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
                options.DefaultNameOrConnectionString = DatabaseConfiguration.ConnectionString;

                // Altera o default isolation level para Unspecified (SqlLite não trabalha com isolationLevel)
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.Unspecified;

                // Habita o suporte ao multitenancy
                options.MultiTenancy(tenancy => tenancy.IsEnabled = true);
            });

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

            app.UseMvcWithDefaultRoute();
            app.UseResponseCompression();

            app.UseSpa(spa =>
            {
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                    spa.UseAngularCliServer(npmScript: "start");
            });

            app.ApplicationServices.MigrateDatabase();
        }
    }
}
