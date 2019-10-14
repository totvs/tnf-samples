using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SuperMarket.FiscalService.Infra.Contexts;

namespace SuperMarket.FiscalService.Web.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly FiscalContext _fiscalContext;

        public MigrationHostedService(FiscalContext fiscalContext)
        {
            _fiscalContext = fiscalContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _fiscalContext.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
