using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Tnf.CarShop.EntityFrameworkCore.Migrator;

public class MigrationHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IServiceScopeFactory _scopeFactory;

    public MigrationHostedService(IServiceScopeFactory scopeFactory, IHostApplicationLifetime applicationLifetime)
    {
        _scopeFactory = scopeFactory;
        _applicationLifetime = applicationLifetime;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationHostedService>>();
        var carShopContext = scope.ServiceProvider.GetRequiredService<CarShopDbContext>();

        logger.LogInformation($"Executing database migration - {nameof(CarShopDbContext)}");

        try
        {
            await carShopContext.Database.MigrateAsync(cancellationToken);

            logger.LogInformation("Database migration executed successfully");
        }
        catch (Exception ex)
        {
            logger.LogInformation("An error has found when performing migration - error: { error }", ex);

            throw new Exception(ex.Message, ex);
        }

        _applicationLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}