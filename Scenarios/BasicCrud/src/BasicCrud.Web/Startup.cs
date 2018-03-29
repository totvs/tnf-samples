using BasicCrud.Application;
using BasicCrud.Domain;
using BasicCrud.Infra;
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Chaveamento de qual banco a aplicação irá usar
            services
                .AddCorsAll("AllowAll")
                .AddApplicationServiceDependency()  // dependencia da camada BasicCrud.Application
                                                    //.AddSqLiteDependency()            // dependencia da camada BasicCrud.Infra.SqLite
                                                    //.AddOracleDependency()            // dependencia da camada BasicCrud.Infra.Oracle
                .AddSqlServerDependency()           // dependencia da camada BasicCrud.Infra.SqlServer
                .AddTnfAspNetCore();                // dependencia do pacote Tnf.AspNetCore

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

                // Recupera a configuração da aplicação
                var configuration = options.Settings.FromJsonFiles(env.ContentRootPath, $"appsettings.{env.EnvironmentName}.json");

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = configuration.GetConnectionString(SqlServerConstants.ConnectionStringName);

                // Altera o default isolation level para Unspecified (SqlLite não trabalha com isolationLevel)
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.Unspecified;

                // Altera o default isolation level para ReadCommitted (ReadUnCommited not supported by Devart)
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

                // Habilita o driver Oracle da Devart (DotConnect for Oracle)
                //options.EnableDevartOracleDriver(useDefaultLicense: true);
            });

            // SqlServer migrate database
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
