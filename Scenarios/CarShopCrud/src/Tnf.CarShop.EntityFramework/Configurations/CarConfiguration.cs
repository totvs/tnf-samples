using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.EntityFrameworkCore.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");

        builder.HasKey(car => car.Id);

        builder.HasOne(car => car.Owner)
            .WithMany(owner => owner.CarsOwned)
            .HasForeignKey(car => car.CustomerId);

        builder.HasOne(car => car.Dealer)
            .WithMany(dealer => dealer.Cars)
            .HasForeignKey(car => car.DealerId);
    }
}