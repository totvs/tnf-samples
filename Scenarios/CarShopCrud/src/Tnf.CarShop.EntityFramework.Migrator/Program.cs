using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

using Tnf.CarShop.EntityFramework.Migrator;
using Tnf.CarShop.EntityFramework.PostgreSql;

var builder = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(configuration =>
    {
        configuration.AddJsonFile($"appsettings.Development.json", optional: false);
        configuration.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.ConfigureTnf(tnf =>
        {
            tnf.DefaultConnectionString(context.Configuration.GetConnectionString("PostgreSql"));
        });

        services.AddEFCorePostgreSql();

        services.AddHostedService<MigrationHostedService>();
    })
    .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

await builder.Build()
    .RunAsync();

