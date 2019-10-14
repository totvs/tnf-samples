using System;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Domain;
using SuperMarket.Backoffice.Crud.Infra;
using SuperMarket.Backoffice.Crud.Web.HostedServices;
using Swashbuckle.AspNetCore.Swagger;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.Crud.Web
{
    public class Startup
    {
        private readonly DatabaseConfiguration _databaseConfiguration;
        private readonly RedisConfiguration _redisConfiguration;
        public Startup(IConfiguration configuration)
        {
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            _redisConfiguration = new RedisConfiguration(configuration);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddCrudInfraDependency(_redisConfiguration)
                .AddCrudDomainDependency()
                .AddTnfAspNetCore();

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Crud API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SuperMarket.Backoffice.Crud.Web.xml"));
                });

            services.AddHostedService<MigrationHostedService>();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação
                options.ConfigureCrudDomain();

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = _databaseConfiguration.ConnectionString;

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

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud API v1");
            });

            app.UseMvcWithDefaultRoute();
            app.UseResponseCompression();

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        }
    }
}
