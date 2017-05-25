using System;
using Abp.AspNetCore;
using Tnf.Sample.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tnf.EntityFrameworkCore;
using Tnf.AspNetCore;
using Tnf.App.EntityFrameworkCore.Localization;
using Microsoft.EntityFrameworkCore;
using Tnf.App.EntityFrameworkCore.Configuration;

namespace Tnf.Sample.Web.Startup
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTnfDbContext<SampleAppDbContext>(options =>
            {
                DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            });

            services.AddTnfDbContext<TnfAppLocalizationDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(options.ConnectionString);
            });

            services.AddTnfDbContext<TnfAppSettingsDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(options.ConnectionString);
            });

            services.AddMvc();
            services.AddSwaggerGen();

            //Configure Abp and Dependency Injection
            return services.AddTnf<SampleWebModule>(options => { });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseTnf(); //Initializes ABP framework.

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUi(); //URL: /swagger/ui
        }
    }
}
