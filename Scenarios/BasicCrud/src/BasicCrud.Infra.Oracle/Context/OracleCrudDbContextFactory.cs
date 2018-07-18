using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Drivers.DevartOracle;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.Oracle.Context
{
    public class OracleCrudDbContextFactory : IDesignTimeDbContextFactory<OracleCrudDbContext>
    {
        public OracleCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CrudDbContext>();

            var configuration = new ConfigurationBuilder()
                                     .SetBasePath(Directory.GetCurrentDirectory())
                                     .AddJsonFile($"appsettings.Development.json", false)
                                     .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            License.Validade(databaseConfiguration.ConnectionString);

            builder.UseOracle(databaseConfiguration.ConnectionString);

            return new OracleCrudDbContext(builder.Options, NullTnfSession.Instance, databaseConfiguration);
        }
    }
}
