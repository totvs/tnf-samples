using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;

namespace Tnf.Sample.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        static DbContextOptionsConfigurer()
        {
        }
        
        public static void Configure(DbContextOptionsBuilder<SampleAppDbContext> dbContextOptions,string connectionString)
        {
            /* This is the single point to configure DbContextOptions for SimpleTaskAppDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
