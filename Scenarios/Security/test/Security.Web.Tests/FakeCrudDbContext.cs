using Security.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Security.Web.Tests
{
    public class FakeCrudDbContext : CrudDbContext
    {
        public FakeCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
