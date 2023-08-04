using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.EntityFrameworkCore.Configurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchases");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Customer)
               .WithMany()
               .HasForeignKey(p => p.CustomerId);

            builder.HasOne(p => p.Car)
                   .WithMany()
                   .HasForeignKey(p => p.CarId);
        }
    }
}
