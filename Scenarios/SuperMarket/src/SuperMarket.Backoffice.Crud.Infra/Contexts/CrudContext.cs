using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace SuperMarket.Backoffice.Crud.Infra.Contexts
{
    public class CrudContext : TnfDbContext
    {
        public CrudContext(DbContextOptions<CrudContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
