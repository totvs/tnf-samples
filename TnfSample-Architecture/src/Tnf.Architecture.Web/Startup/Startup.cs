using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using Tnf.App.AspNetCore;
using Tnf.App.EntityFrameworkCore.Configuration;
using Tnf.App.EntityFrameworkCore.Localization;
using Tnf.Architecture.Domain.Configuration;
using Tnf.Architecture.Dto;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.AspNetCore;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var sqlConnection = new SqlConnection(_appConfiguration.GetConnectionString(AppConsts.ConnectionStringName));

            services.AddTnfDbContext<LegacyDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(sqlConnection);
            });

            services.AddTnfDbContext<ArchitectureDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(sqlConnection);
            });

            services.AddTnfDbContext<TnfAppLocalizationDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(sqlConnection);
            });

            services.AddTnfDbContext<TnfAppSettingsDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(sqlConnection);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc();
            services.AddSwaggerGen();

            //Configure Tnf and Dependency Injection
            return services.AddTnfApp<WebModule>(options => { });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseTnf(); //Initializes Tnf framework.

            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            // Add CORS middleware before MVC
            app.UseCors("AllowAll");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUi(); //URL: /swagger/ui

            app.UseSwagger((httpRequest, swaggerDoc) =>
            {
                swaggerDoc.Host = httpRequest.Host.Value;
            });
        }
    }
}
