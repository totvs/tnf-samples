using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JobSchedulerClient.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = "Job Scheduler Client Sample";

            await CreateHostBuilder(args)
                .Build()
                .RunAsync();

            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(config =>
                {
                    config.UseStartup<Startup>();
                })
                .UseSerilog((context, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                });
    }
}
