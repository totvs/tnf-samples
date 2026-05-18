using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TnfZero.EntityFramework;
using TnfZero.EntityFramework.PostgreSQL;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("Default")
                               ?? throw new InvalidOperationException(
                                   "Connection string 'Default' not found in configuration.");

        services.AddTnfDbContext<TnfZeroDbContext, PostgreSqlTnfZeroDbContext>(c =>
        {
            c.DbContextOptions.UseNpgsql(connectionString);
        });
    })
    .Build();

await using var scope = host.Services.CreateAsyncScope();
var db = scope.ServiceProvider.GetRequiredService<TnfZeroDbContext>();

Console.WriteLine("Applying EF Core migrations...");
await db.Database.MigrateAsync();
Console.WriteLine("Migrations applied successfully.");