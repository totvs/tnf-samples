using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace SuperMarket.Backoffice.FiscalService.Infra.Contexts
{
    public class FiscalContext : TnfDbContext
    {
        public FiscalContext(DbContextOptions<FiscalContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
