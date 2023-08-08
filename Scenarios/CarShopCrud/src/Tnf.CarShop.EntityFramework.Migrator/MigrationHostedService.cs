using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        var carShopContext = scope.ServiceProvider.GetRequiredService<CarShopDbContext>();
        await carShopContext.Database.MigrateAsync(cancellationToken);

        _applicationLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}