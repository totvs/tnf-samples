﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace SuperMarket.FiscalService.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = typeof(Program).Namespace;

            await CreateHostBuilder(args)
                .Build()
                .RunAsync();

            Log.CloseAndFlush();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                })
                .UseSerilog((context, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                });
    }
}
