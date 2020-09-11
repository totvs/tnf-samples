using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HealthMonitoring.Web.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace HealthMonitoring.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsAll("AllowAll");

            // dependencia do pacote Tnf.AspNetCore
            services.AddTnfAspNetCore(builder =>
            {
                builder.ConfigureLocalization();
            });

            // Registro de serviço de health-check customizado
            services.AddHealthChecks()
                .AddCheck<ExampleHealthCheck>(
                    "example_health_check",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] { "example" });

            // Dependência do pacote de coleta de métricas Tnf.AspNetCore.AppMetrics
            services.AddTnfMetrics(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Health Monitoring API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "HealthMonitoring.Web.xml"));
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddResponseCompression();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseTnfAspNetCore();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Health Monitoring API v1");
            });

            app.UseResponseCompression();

            // Configura endpoints e middleware de publicação de métricas
            app.UseTnfMetrics();

            app.UseEndpoints(endpoint =>
            {
                // Ignorando os predicados customizados durante liveness check
                endpoint.MapTnfLivenessCheck(new HealthCheckOptions() { Predicate =
                    _ => false
                });

                // Filtrando os predicados desejados para Readiness Check através de Tag
                endpoint.MapTnfReadinessCheck(new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("example")
                });

                endpoint.MapDefaultControllerRoute();
            });
        }
    }
}
