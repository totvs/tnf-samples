using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Tnf.CarShop.EntityFrameworkCore.PostgreSql;

public class PostgreSqlCarShopDbContext : CarShopDbContext
{
    //See more: https://www.npgsql.org/doc/types/datetime.html
    private const string NpgsqlEnableLegacyTimestampBehavior = "Npgsql.EnableLegacyTimestampBehavior";

    public PostgreSqlCarShopDbContext(DbContextOptions<CarShopDbContext> options, ITnfSession session)
        : base(options, session)
    {
        AppContext.SetSwitch(NpgsqlEnableLegacyTimestampBehavior, true);
    }
}