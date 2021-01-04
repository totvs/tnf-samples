using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SGDP.Application;
using SGDP.Domain;
using SGDP.Infra;
using SGDP.Infra.Context;
using SGDP.Web.HostedServices;
using Tnf.Sgdp;

namespace SGDP.Web
{
    public class Startup
    {
        DatabaseConfiguration DatabaseConfiguration { get; }

        public Startup(IConfiguration configuration)
        {
            DatabaseConfiguration = new DatabaseConfiguration(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddApplicationServiceDependency();  // dependencia da camada SGDP.Application

            services.AddTnfAspNetCore(builder =>
            {
                builder.UseDomainLocalization();

                // Configura a connection string da aplicação
                builder.DefaultConnectionString(DatabaseConfiguration.ConnectionString);

                if (DatabaseConfiguration.DatabaseType == DatabaseType.PostgreSQL)
                    builder.EnableDevartPostgreSQLDriver();
            });

            services.AddTnfSgdp(sgdp =>
            {
                sgdp.ConfigureOptions(Configuration)
                    .AddSgdpDataService<SampleSgdpDataService>()
                    .AddSgdpMaskService<SampleSgdpMaskService>()
                    .UseEFCoreLogBuffer<OrderDbContext>(Configuration);
            });

            services.AddSingleton(DatabaseConfiguration);

            if (DatabaseConfiguration.DatabaseType == DatabaseType.PostgreSQL)
                services.AddPostgreSQLDependency();
            else
                throw new NotSupportedException("No database configuration found");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SGDP Sample API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SGDP.Web.xml"));
            });

            services.AddResponseCompression();

            services.AddHostedService<MigrationHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseTnfAspNetCore();

            app.UseResponseCompression();

            app.UseRouting();

            app.UseAuthorization();

            app.UseTnfAspNetCoreSecurity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
