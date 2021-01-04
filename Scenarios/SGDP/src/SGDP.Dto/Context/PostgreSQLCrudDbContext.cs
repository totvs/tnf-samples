using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace SGDP.Infra.Context
{
    public class PostgreSQLCrudDbContext : OrderDbContext
    {
        public PostgreSQLCrudDbContext(DbContextOptions<OrderDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
