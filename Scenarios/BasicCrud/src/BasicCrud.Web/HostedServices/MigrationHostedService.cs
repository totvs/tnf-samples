using System.Threading;
using System.Threading.Tasks;
using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BasicCrud.Web.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly CrudDbContext _crudDbContext;

        public MigrationHostedService(CrudDbContext crudDbContext)
        {
            _crudDbContext = crudDbContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _crudDbContext.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
