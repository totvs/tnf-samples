using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Tnf.SmartX.EntityFramework.PostgreSql;

public class PostgreSqlCompanyDbContext : CompanyDbContext
{
    public PostgreSqlCompanyDbContext(
        DbContextOptions<CompanyDbContext> options,
        ITnfSession session)
        : base(options, session)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}
