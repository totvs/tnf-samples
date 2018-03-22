using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Infra;
using Tnf.Caching.Redis;
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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCrudInfraDependency()
                .AddCrudDomainDependency()
                .AddTnfAspNetCore();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSwaggerGen();

            // Redis configuration
            var databaseIndex = Convert.ToInt32(Configuration["DatabaseIndex"]);
            var redisConnectionString = Configuration["RedisConnectionString"];

            services.AddTnfRedisCache(builder => builder

                // Nome para o qual o cache será registrado no DI
                .UseDefaultName("Default")

                // Para customizar a serialização implemente a interface Tnf.Caching.Redis.IRedisSerializer
                // e passe a instancia do seu serializador utilizando o método .UseSerializer()
                .UseJsonSerializer()
                .UseCacheOptions(new CacheOptions()
                {
                    LogDeletedKeys = true,                // Exibir no log quando uma key for deletada
                })
                .UseDatabase(databaseIndex)                     // Redis Database Id
                .UseConnectionString(redisConnectionString));   // Redis Connection String

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

            logger.LogInformation("Start application ...");
        }
    }
}
