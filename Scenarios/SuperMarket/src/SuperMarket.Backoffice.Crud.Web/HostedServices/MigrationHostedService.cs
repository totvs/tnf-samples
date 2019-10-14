using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SuperMarket.Backoffice.Crud.Infra.Contexts;

namespace SuperMarket.Backoffice.Crud.Web.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly CrudContext _crudContext;

        public MigrationHostedService(CrudContext crudContext)
        {
            _crudContext = crudContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _crudContext.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
