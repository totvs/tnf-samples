using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;
using Tnf.SmartX.Domain.CodeFirst.Entities;
using Tnf.SmartX.EntityFramework.Configurations;

namespace Microsoft.EntityFrameworkCore;

public class CustomerDbContext(DbContextOptions options, ITnfSession session) : TnfDbContext(options, session)
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<DeliveryEntity> Deliveries { get; set; }
    public DbSet<DeliveryItemEntity> DeliveriesItems { get; set; }
    public DbSet<StateEntity> States { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AddressEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryItemConfiguration());
        modelBuilder.ApplyConfiguration(new StateEntityConfiguration());
    }
}
