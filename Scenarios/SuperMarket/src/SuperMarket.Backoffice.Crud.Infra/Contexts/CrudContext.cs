using Microsoft.EntityFrameworkCore;
using SuperMarket.Backoffice.Crud.Infra.Mappers;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerMapper());
            modelBuilder.ApplyConfiguration(new ProductMapper());
        }
    }
}
