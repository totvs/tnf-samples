using Security.Application;
using Security.Domain;
using Security.Domain.Entities;
using Security.Dto;
using Security.Infra;
using Security.Infra.Oracle;
using Security.Infra.SqLite;
using Security.Infra.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Tnf.Configuration;
using Tnf.Security.Rac.Configuration;
using System.Threading;

namespace Security.Web
{
    public class Startup
    {
        DatabaseConfiguration DatabaseConfiguration { get; }
        TnfRacOptions RacOptions { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            DatabaseConfiguration = new DatabaseConfiguration(configuration);
            RacOptions = env.LoadTnfRacOptions(new CancellationTokenSource(), $"racsettings.{env.EnvironmentName}.json");
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddApplicationServiceDependency()      // dependencia da camada Security.Application
                .AddTnfAspNetCore()                     // dependencia do pacote Tnf.AspNetCore
                .AddTnfAspNetCoreSecurity(RacOptions);

            if (DatabaseConfiguration.DatabaseType == DatabaseType.SqlServer)
                services.AddSqlServerDependency();
            else if (DatabaseConfiguration.DatabaseType == DatabaseType.Sqlite)
                services.AddSqLiteDependency();
            else if (DatabaseConfiguration.DatabaseType == DatabaseType.Oracle)
                services.AddOracleDependency();
            else
                throw new NotSupportedException("No database configuration found");

            services
                .AddResponseCompression()
                .AddSwaggerGen(c => c.AddSwaagerRacSecurity(RacOptions));

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

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

                options.MultiTenancy(tenancy => tenancy.IsEnabled = true);

                // Habilita o driver Oracle da Devart (DotConnect for Oracle)
                if (DatabaseConfiguration.DatabaseType == DatabaseType.Oracle)
                    options.EnableDevartOracleDriver(useDefaultLicense: true);
            });

            app.UseTnfAspNetCoreSecurity();

            app.ApplicationServices.MigrateDatabase();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.UseSwaggerRacSecurity(RacOptions));

            app.UseMvcWithDefaultRoute();
            app.UseResponseCompression();

            // Habilita o uso do UnitOfWork em todo o request
            app.UseTnfUnitOfWork();

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        }
    }
}
