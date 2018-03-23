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

            // Redis configuration
            var databaseId = Convert.ToInt32(Configuration["DatabaseIndex"]);
            var connectionString = Configuration["RedisConnectionString"];

            services.AddTnfRedisCache(builder => builder

                // Nome para o qual o cache será registrado no DI
                .UseDefaultName("Default")

                // Para customizar a serialização implemente a interface Tnf.Caching.Redis.IRedisSerializer
                // e passe a instancia do seu serializador utilizando o método .UseSerializer()
                .UseJsonSerializer()
                .UseCacheOptions(new CacheOptions()
                {
                    LogDeletedKeys = true,                // Exibir no log quando uma key for deletada
                    ObjectSizeWarning = 10                // Exibir no log quando um objeto ultrapassar o valor definido nessa opção
                })
                .UseDatabase(databaseId)                  // Redis Database Id
                .UseConnectionString(connectionString));  // Redis Connection String

            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            Console.Title = "RedisCache";

            var environmentName = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
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
