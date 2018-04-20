using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.SqLite.Context
{
    public class SqliteCrudDbContext : CrudDbContext
    {

        public SqliteCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}