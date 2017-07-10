using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Tnf.App.AspNetCore;
using Tnf.Architecture.Web.Startup;
using Tnf.AspNetCore;
using Tnf.AspNetCore.TestBase;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Web.Tests.App
{
    public class StartupTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add controllers in test
            services
                .AddMvcCore()
                .AddApplicationPart(typeof(WebModule).GetAssembly());

            // Add support to Entity Framework In Memory
            services.AddEntityFrameworkInMemoryDatabase();

            // Configure Tnf and Dependency Injection
            return services.AddTnfApp<AppTestModule>(options =>
            {
                //Test setup
                options.SetupTest();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseTnf(); //Initializes Tnf framework.

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
