using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.EntityFrameworkCore.Configurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchases");

        builder.HasKey(purchase => purchase.Id);

        builder.HasOne(purchase => purchase.Customer)
            .WithMany()
            .HasForeignKey(purchase => purchase.CustomerId);

        builder.HasOne(purchase => purchase.Car)
            .WithMany()
            .HasForeignKey(purchase => purchase.CarId);
    }
}