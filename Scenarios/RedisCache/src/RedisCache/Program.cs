using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RedisCache.CachedValues;
using RedisCache.Interfaces;

using Serilog;

using System;

using Tnf.Caching.Redis;

namespace RedisCache
{
    class Program
    {
        private static IConfigurationRoot Configuration;

        static IServiceProvider ConfigureServices()
        {
            // Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            var services = new ServiceCollection()
                .AddLogging(config => config.AddSerilog(Log.Logger));

            services
                .AddTransient<IProductRepository, ProductRepositoryInMemory>()
                .AddTransient<IProductService, ProductService>();

            services.AddTnfRedisCache(Configuration);

            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            Console.Title = "RedisCache";

            var environmentName = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");

            Configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                .Build();

            var provider = ConfigureServices();

            var productServive = provider.GetRequiredService<IProductService>();

            var products = productServive.GetProducts();

            var newProduct = new Product(Guid.NewGuid(), "Novo Produto", ProductRepositoryInMemory.DefaultCategory);

            productServive.Add(newProduct);

            products = productServive.GetProducts();

            productServive.Update(new Product(ProductRepositoryInMemory.ProductKey, "Produto Atualizado", ProductRepositoryInMemory.DefaultCategory));

            products = productServive.GetProducts();

            productServive.Delete(newProduct.Id);

            products = productServive.GetProducts();

            Console.ReadKey();

            Log.CloseAndFlush();
        }
    }
}
