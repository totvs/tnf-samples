using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SuperMarket.Backoffice.Sales.Infra.Contexts;

namespace SuperMarket.Backoffice.Sales.Web.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly SalesContext _salesContext;

        public MigrationHostedService(SalesContext salesContext)
        {
            _salesContext = salesContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _salesContext.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
