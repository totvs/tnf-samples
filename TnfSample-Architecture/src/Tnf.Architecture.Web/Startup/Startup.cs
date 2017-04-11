using System;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tnf.AspNetCore;
using Tnf.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.App.EntityFrameworkCore.Localization;
using Microsoft.EntityFrameworkCore;
using Tnf.App.EntityFrameworkCore.Configuration;

namespace Tnf.Architecture.Web.Startup
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTnfDbContext<ArchitectureDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(options.ConnectionString);
            });

            services.AddTnfDbContext<TnfAppLocalizationDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(options.ConnectionString);
            });

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

            //services.AddRouting();
            services.AddSwaggerGen();

            //Configure Abp and Dependency Injection
            return services.AddTnf<WebModule>(options =>
            {   
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseLog4Net().WithConfig("log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseTnf(); //Initializes Tnf framework.
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

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
