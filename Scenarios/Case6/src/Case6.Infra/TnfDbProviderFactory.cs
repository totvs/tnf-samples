using Tnf.Configuration;
using Devart.Data.Oracle;
using System.Data.Common;

namespace Case5.Web
{
    public class TnfDbProviderFactory : OracleProviderFactory
    {
        private readonly ITnfConfiguration tnfConfiguration;

        public TnfDbProviderFactory(ITnfConfiguration tnfConfiguration)
        {
            this.tnfConfiguration = tnfConfiguration;
        }

        public override DbConnection CreateConnection()
        {
            var connection = base.CreateConnection();
            connection.ConnectionString = tnfConfiguration.DefaultNameOrConnectionString;
            return connection;
        }
    }
}