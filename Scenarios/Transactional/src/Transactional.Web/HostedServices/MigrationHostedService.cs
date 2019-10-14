using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Transactional.Infra.Context;

namespace Transactional.Web.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly PurchaseOrderContext _purchaseOrderContext;

        public MigrationHostedService(PurchaseOrderContext purchaseOrderContext)
        {
            _purchaseOrderContext = purchaseOrderContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _purchaseOrderContext.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
