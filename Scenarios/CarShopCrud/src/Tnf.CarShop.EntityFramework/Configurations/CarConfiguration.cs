using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.EntityFrameworkCore.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Owner)
                .WithMany(o => o.CarsOwned)
                .HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.Dealer)
                .WithMany(d => d.Cars)
                .HasForeignKey(x => x.DealerId);
        }
    }
}
