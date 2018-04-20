﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace Dapper.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Dapper";

            var host = WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);

                    config.AddEnvironmentVariables();
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
                .Build();

            host.Run();

            Log.CloseAndFlush();
        }
    }
}