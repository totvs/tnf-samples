using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace Case5.Infra.Context.Migration
{
    public class EmployeeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
    {
        public EmployeeDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EmployeeDbContext>();

            var assemblyPath = new DirectoryInfo(typeof(EmployeeDbContextFactory).Assembly.Location).Parent.FullName;

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(assemblyPath)
                                    .AddJsonFile($"appsettings.json", true)
                                    .Build();

            builder.UseOracle(configuration.GetConnectionString(InfraConsts.ConnectionStringName));

            return new EmployeeDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}