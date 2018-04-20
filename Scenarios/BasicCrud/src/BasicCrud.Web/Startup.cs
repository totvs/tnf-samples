using BasicCrud.Application;
using BasicCrud.Domain;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto;
using BasicCrud.Infra;
using BasicCrud.Infra.Oracle;
using BasicCrud.Infra.SqLite;
using BasicCrud.Infra.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using Tnf.Configuration;

namespace BasicCrud.Web
{
    public class Startup
    {
        DatabaseConfiguration DatabaseConfiguration { get; }

        public Startup(IConfiguration configuration)
        {
            DatabaseConfiguration = new DatabaseConfiguration(configuration);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Chaveamento de qual banco a aplicação irá usar
            services
                .AddCorsAll("AllowAll")
                .AddApplicationServiceDependency()  // dependencia da camada BasicCrud.Application
                .AddTnfAspNetCore();                // dependencia do pacote Tnf.AspNetCore

            if (DatabaseConfiguration.DatabaseType == DatabaseType.SqlServer)
                services.AddSqlServerDependency();
            else if (DatabaseConfiguration.DatabaseType == DatabaseType.Sqlite)
                services.AddSqLiteDependency();
            else if (DatabaseConfiguration.DatabaseType == DatabaseType.Oracle)
                services.AddOracleDependency();
            else
                throw new NotSupportedException("No database configuration found");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Basic Crud API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "BasicCrud.Web.xml"));
            });

            services.AddResponseCompression();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configura o use do AspNetCore do Tnf
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

                // Altera o default isolation level para ReadCommitted (ReadUnCommited not supported by Devart)
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

                // Habilita o driver Oracle da Devart (DotConnect for Oracle)
                if (DatabaseConfiguration.DatabaseType == DatabaseType.Oracle)
                    options.EnableDevartOracleDriver(useDefaultLicense: true);
            });

            app.ApplicationServices.MigrateDatabase();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Habilita o uso do UnitOfWork em todo o request
            app.UseTnfUnitOfWork();

            // Add CORS middleware before MVC
            app.UseCors("AllowAll");

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basic Crud API v1");
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
