using BasicCrud.Application;
using BasicCrud.Domain;
using BasicCrud.Infra;
using BasicCrud.Infra.Oracle;
using BasicCrud.Infra.SqLite;
using BasicCrud.Infra.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Transactions;
using Tnf.Configuration;

namespace BasicCrud.Web
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Chaveamento de qual banco a aplicação irá usar
            services
                //.AddSqLiteDependency()            // dependencia da camada BasicCrud.Infra.SqLit
                //.AddOracleDependency()            // dependencia da camada BasicCrud.Infra.Oracle
                .AddSqlServerDependency()           // dependencia da camada BasicCrud.Infra.SqlServer
                .AddApplicationServiceDependency()  // dependencia da camada BasicCrud.Application
                .AddTnfAspNetCore();                // dependencia do pacote Tnf.AspNetCore

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação
                options.AddDomainLocalization();

                // Recupera a configuração da aplicação
                var configuration = options.Settings.FromJsonFiles(env.ContentRootPath, "appsettings.json");

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = configuration.GetConnectionString(SqLiteConstants.ConnectionStringName);

                // Opção para usar SqLite
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.Unspecified;

                // Altera o default isolation level para ReadCommitted (ReadUnCommited not supported by Devart)
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

                // Habilita o driver Oracle da Devart (DotConnect for Oracle)
                //options.EnableDevartOracleDriver(useDefaultLicense: true);
            });

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
        }
    }
}
