using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Tnf.App.AspNetCore.TestBase;
using Tnf.Architecture.Web.Startup;
using Tnf.AspNetCore;

namespace Tnf.Architecture.Web.Tests.App
{
    public class StartupTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add support to Entity Framework In Memory
            services.AddEntityFrameworkInMemoryDatabase();

            // Configure Tnf and Dependency Injection
            return services.AddTnfAppTestBase<AppTestModule, WebModule>();
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
