using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace BasicCrud.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = typeof(Program).Namespace;

            var hostConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json")
                .Build();

            var host = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(hostConfig)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);

                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.WithMachineName()
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .CreateLogger();
                })
                .UseStartup<Startup>()
                .UseSerilog()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseSetting("detailedErrors", "true")
                .Build();

            host.Run();

            Log.CloseAndFlush();
        }
    }
}
