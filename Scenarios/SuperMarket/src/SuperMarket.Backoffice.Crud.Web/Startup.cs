using System;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Infra;
using Swashbuckle.AspNetCore.Swagger;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.Crud.Web
{
    public class Startup
    {
        private readonly IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var databaseIndex = Convert.ToInt32(Configuration["DatabaseIndex"]);
            var redisConnectionString = Configuration["RedisConnectionString"];

            services
                .AddCrudInfraDependency(databaseIndex, redisConnectionString)
                .AddCrudDomainDependency()
                .AddTnfAspNetCore();

            services.AddCorsAll("AllowAll");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Crud API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SuperMarket.Backoffice.Crud.Web.xml"));
            });

            services.AddResponseCompression();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação
                options.ConfigureCrudDomain();

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = Configuration.GetConnectionString(Constants.ConnectionStringName);

                // ---------- Configurações de Unit of Work a nível de aplicação

                // Por padrão um Uow é transacional: todas as operações realizadas dentro de um Uow serão
                // comitadas ou desfeitas em caso de erro
                options.UnitOfWorkOptions().IsTransactional = true;

                // IsolationLevel default de cada transação criada. (Precisa da configuração IsTransactional = true para funcionar)
                options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

                // Escopo da transação. (Precisa da configuração IsTransactional = true para funcionar)
                options.UnitOfWorkOptions().Scope = TransactionScopeOption.Required;

                // ----------
            });

            logger.LogInformation("Running migrations ...");

            app.ApplicationServices.MigrateDatabase();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Habilita o uso do UnitOfWork em todo o request
            app.UseTnfUnitOfWork();

            // Add CORS middleware before MVC
            app.UseCors("AllowAll");
            app.UseMvcWithDefaultRoute();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud API v1");
            });

            app.UseResponseCompression();

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            logger.LogInformation("Start application ...");
        }
    }
}
