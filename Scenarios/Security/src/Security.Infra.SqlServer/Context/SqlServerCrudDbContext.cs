using Security.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Security.Infra.SqlServer.Context
{
    public class SqlServerCrudDbContext : CrudDbContext
    {
        public SqlServerCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}