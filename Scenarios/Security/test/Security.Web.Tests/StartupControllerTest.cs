using Security.Application.Services.Interfaces;
using Security.Web.Tests.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace Security.Web.Tests
{
    public class StartupControllerTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Configura o setup de teste para AspNetCore
            services.AddTnfAspNetCoreSetupTest();

            // Registro dos serviços de Mock
            services.AddTransient<ICustomerAppService, CustomerAppServiceMock>();
            services.AddTransient<IProductAppService, ProductAppServiceMock>();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest();

            // Authentication
            app.Use(async (context, next) =>
            {
                context.User = new ClaimsPrincipal();
                await next.Invoke();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
