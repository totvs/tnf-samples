using ProdutoXyz.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace ProdutoXyz.Infra.SqLite.Context
{
    public class SqliteCrudDbContext : CrudDbContext
    {

        public SqliteCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}