using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Threading.Tasks;
using System.Transactions;
using Tnf.Configuration;
using Transacional.Domain;
using Transactional.Domain;
using Transactional.Infra;

namespace Transactional.Web
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddDomainDependency()              // dependencia da camada Transactional.Domain
                .AddInfraDependency()               // dependencia da camada Transactional.Infra
                .AddTnfAspNetCore();                // dependencia do pacote Tnf.AspNetCore

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Transactional API", Version = "v1" });
            });

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação
                options.ConfigureLocalization();

                // Recupera a configuração da aplicação
                var configuration = options.Settings.FromJsonFiles(env.ContentRootPath, $"appsettings.{env.EnvironmentName}.json");

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = configuration.GetConnectionString(Constants.ConnectionStringName);


                // ---------- Configurações de Unit of Work a nível de aplicação

                // Por padrão um Uow é transacional: todas as operações realizadas dentro de um Uow serão
                // comitadas ou desfeitas em caso de erro
                options.UnitOfWorkOptions().IsTransactional = true;

                // IsolationLevel default de cada transação criada. (Precisa da configuração IsTransactional = true para funcionar)
                options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

                // Escopo da transação. (Precisa da configuração IsTransactional = true para funcionar)
                options.UnitOfWorkOptions().Scope = TransactionScopeOption.Required;

                // Timeout que será aplicado (se este valor for informado) para toda nova transação criada
                // Não é indicado informar este valor pois irá afetar toda a aplicação.
                options.UnitOfWorkOptions().Timeout = TimeSpan.FromSeconds(5);

                // ----------
            });

            logger.LogInformation("Running migrations ...");

            app.ApplicationServices.MigrateDatabase();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Add CORS middleware before MVC
            app.UseCors("AllowAll");

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transactional API v1");
            });

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            logger.LogInformation("Start application ...");
        }
    }
}
