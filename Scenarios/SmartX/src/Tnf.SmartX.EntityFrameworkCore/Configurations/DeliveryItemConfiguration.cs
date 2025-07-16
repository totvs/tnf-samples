using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations;

public class DeliveryItemConfiguration : IEntityTypeConfiguration<DeliveryItemEntity>, ISXEntityConfiguration
{
    public void Configure(EntityTypeBuilder<DeliveryItemEntity> builder)
    {
        builder.ToTable("DeliveryItems");

        builder.HasKey(di => di.Id);

        builder.Property(di => di.ProductName)
            .IsRequired()
            .SxTitle("Nome do produto");

        builder.Property(di => di.Quantity)
            .IsRequired()
            .SxTitle("Quantidade");

        builder.Property(di => di.WeightKg)
            .IsRequired()
            .SxTitle("Peso (kg)");

        builder.Property(di => di.CreationTime)
            .SxHidden()
            .IsRequired()
            .HasDefaultValue(DateTime.MinValue);

        builder.Property(di => di.LastModificationTime)
            .SxHidden();

        builder.Property(di => di.Id)
            .SxHidden();

        builder.Property(di => di.DeliveryId)
            .SxHidden();
    }
}
