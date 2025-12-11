using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ProdutoXyz.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = "Produto Xyz API";

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
