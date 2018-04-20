using BasicCrud.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace BasicCrud.Web.Tests
{
    public class FakeCrudDbContext : CrudDbContext
    {
        public FakeCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
