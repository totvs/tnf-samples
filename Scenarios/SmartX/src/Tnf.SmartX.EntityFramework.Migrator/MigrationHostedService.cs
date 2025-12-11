using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Tnf.SmartX.EntityFramework.Migrator;

public class MigrationHostedService(IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime applicationLifetime) : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationHostedService>>();
        var companyDbContext = scope.ServiceProvider.GetService<CompanyDbContext>();
        var customerDbContext = scope.ServiceProvider.GetService<CustomerDbContext>();

        try
        {
            logger.LogInformation("Applying migration from CompanyDbContext... ");
            await companyDbContext.Database.MigrateAsync(cancellationToken: cancellationToken);

            logger.LogInformation("Applying migration from CustomerDBContext...");
            await customerDbContext.Database.MigrateAsync(cancellationToken: cancellationToken);

            logger.LogInformation("Database migrations completed!");
        }
        catch (Exception ex)
        {
            logger.LogInformation("An error has found when performing migration - error: { error }", ex);

            throw;
        }

        applicationLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
