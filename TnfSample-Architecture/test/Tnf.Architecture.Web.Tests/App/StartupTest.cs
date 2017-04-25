using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tnf.AspNetCore.TestBase;
using Tnf.AspNetCore;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Web.Tests.App
{
    public class StartupTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var mvc = services.AddMvc();
            
            services.AddEntityFrameworkInMemoryDatabase();

            mvc.PartManager.ApplicationParts.Add(new AssemblyPart(typeof(TnfAspNetCoreModule).GetAssembly()));

            //Configure Tnf and Dependency Injection
            return services.AddTnf<AppTestModule>(options =>
            {
                //Test setup
                options.SetupTest();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseTnf(); //Initializes Tnf framework.

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
