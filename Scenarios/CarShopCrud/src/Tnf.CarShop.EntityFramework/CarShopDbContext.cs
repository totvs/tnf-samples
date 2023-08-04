using Microsoft.EntityFrameworkCore;

using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.EntityFrameworkCore.Configurations;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Tnf.CarShop.EntityFrameworkCore
{
    public class CarShopDbContext : TnfDbContext
    {
        public DbSet<Car> Cars { get; set; }

        public CarShopDbContext(DbContextOptions<CarShopDbContext> options, ITnfSession session) 
            : base(options, session) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new DealerConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
        }
    }
}
