using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SuperMarket.Backoffice.Crud.Domain;
using System.IO;
using Tnf.Runtime.Session;

namespace SuperMarket.Backoffice.Crud.Infra.Contexts
{
    public class CrudContextFactory : IDesignTimeDbContextFactory<CrudContext>
    {
        public CrudContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CrudContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", false)
                                    .Build();

            builder.UseSqlServer(configuration.GetConnectionString(Constants.ConnectionStringName));

            return new CrudContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
