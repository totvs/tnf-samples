using Tnf.Sample.Configuration;
using Tnf.Sample.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Tnf.Sample.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class SampleAppDbContextFactory : IDbContextFactory<SampleAppDbContext>
    {
        public SampleAppDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<SampleAppDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(SampleAppConsts.ConnectionStringName)
                );

            return new SampleAppDbContext(builder.Options);
        }
    }
}