using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.EntityFrameworkCore.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(customer => customer.Id);

        builder.HasOne(customer => customer.Store)
            .WithMany(store => store.Customers)
            .HasForeignKey(customer => customer.TenantId);
    }
}