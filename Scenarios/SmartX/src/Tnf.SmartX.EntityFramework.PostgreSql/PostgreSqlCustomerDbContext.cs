using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Tnf.SmartX.EntityFramework.PostgreSql;

public class PostgreSqlCustomerDbContext : CustomerDbContext
{
    public PostgreSqlCustomerDbContext(
        DbContextOptions<CustomerDbContext> options,
        ITnfSession session)
    : base(options, session)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}
