using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.Oracle.Context
{
    public class OracleCrudDbContext : CrudDbContext
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public OracleCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session, DatabaseConfiguration databaseConfiguration)
            : base(options, session)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        static OracleCrudDbContext()
        {
            DevartOracleSettings.SetDefaultSettings();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(_databaseConfiguration.DefaultSchema);
        }
    }
}