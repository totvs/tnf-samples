using System;
using System.Collections.Generic;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Tnf.AspNetCore;
using Tnf.Sample.Web.Configuration;

namespace Tnf.Sample.Web.Startup
{
    public class Startup
    {
        public Startup(ILoggerFactory loggerFactory, IHostingEnvironment environment)
        {
            var serilog = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .Enrich.FromLogContext()
               .WriteTo.File(@"c:\logs\identityserver4_log_tnf.txt");

            if (environment.IsDevelopment())
            {
                serilog.WriteTo.LiterateConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message}{NewLine}{Exception}{NewLine}");
            }

            loggerFactory
                .WithFilter(new FilterLoggerSettings
                {
                    { "IdentityServer", LogLevel.Debug },
                    { "Microsoft", LogLevel.Information },
                    { "System", LogLevel.Error },
                })
                .AddSerilog(serilog.CreateLogger());
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // --------------------------------------------------------------------------
            // IdentityServer4
            // --------------------------------------------------------------------------

            const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer4.EntityFramework;trusted_connection=yes;";

            services.AddIdentityServer()
                //.AddInMemoryClients(Clients.Get())
                //.AddInMemoryIdentityResources(Resources.GetIdentityResources())
                //.AddInMemoryApiResources(Resources.GetApiResources())
                .AddTemporarySigningCredential()

                .AddTestUsers(TestUsers.Users)


                .AddConfigurationStore(builder =>
                    builder.UseSqlServer(connectionString))

                .AddOperationalStore(builder =>
                    builder.UseSqlServer(connectionString));

            // --------------------------------------------------------------------------

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });


            //Configure Abp and Dependency Injection
            return services.AddTnf<SampleWebModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseLog4Net().WithConfig("log4net.config")
                );
            });

            //return services.BuildServiceProvider();
        }

        public void Configure(IServiceProvider serviceProvider, IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //var t = serviceProvider.GetService(typeof(TestUserStore));

            app.UseTnf(); //Initializes ABP framework.

            // --------------------------------------------------------------------------
            // IdentityServer4
            // --------------------------------------------------------------------------
            app.UseIdentityServer();

            // --------------------------------------------------------------------------
            // IdentityServer4.AccessTokenValidation
            // --------------------------------------------------------------------------

            app.UseCors(policy =>
            {
                policy.WithOrigins(
                    "http://localhost:28895",
                    "http://localhost:7017");

                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders("WWW-Authenticate");
            });

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = Constants.Authority,
                AllowedScopes = { "api1" },

                // The MetadataAddress or Authority must use HTTPS unless disabled for 
                // development by setting RequireHttpsMetadata=false.
                RequireHttpsMetadata = false,

                EnableCaching = false,

                ApiName = "api1",
                ApiSecret = "secret"
            });

            // --------------------------------------------------------------------------

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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
