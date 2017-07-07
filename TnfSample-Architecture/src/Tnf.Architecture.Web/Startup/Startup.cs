using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Tnf.App.AspNetCore;
using Tnf.App.EntityFrameworkCore.Configuration;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.AspNetCore;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.Web.Startup
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTnfDbContext<LegacyDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(options.ConnectionString);
            });

            services.AddTnfDbContext<ArchitectureDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(options.ConnectionString);
            });

            //services.AddTnfDbContext<TnfAppLocalizationDbContext>(options =>
            //{
            //    //options.DbContextOptions.UseSqlServer(sqlConnection);
            //    options.DbContextOptions.UseSqlServer(options.ConnectionString);
            //});

            services.AddTnfDbContext<TnfAppSettingsDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(options.ConnectionString);
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
            services.ConfigureSwaggerGen(x =>
            {
                x.IgnoreObsoleteProperties();
                x.IgnoreObsoleteActions();
                x.DescribeAllEnumsAsStrings();
            });
            services.AddSwaggerGen();

            // Configure Tnf and Dependency Injection
            return services.AddTnfApp<WebModule>(options => { });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Initializes Tnf framework.
            app.UseTnf();

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

            app.UseSwaggerUi();
            app.UseSwagger((httpRequest, swaggerDoc) =>
            {
                swaggerDoc.Host = httpRequest.Host.Value;
            });
        }
    }
}
