using Microsoft.EntityFrameworkCore;

using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Tnf.CarShop.EntityFrameworkCore
{
    public class CarShopDbContext : TnfDbContext
    {
        public CarShopDbContext(DbContextOptions<CarShopDbContext> options, ITnfSession session) 
            : base(options, session) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
