using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations;

public class DeliveryEntityConfiguration : IEntityTypeConfiguration<DeliveryEntity>, ISXEntityConfiguration
{
    public void Configure(EntityTypeBuilder<DeliveryEntity> builder)
    {
        builder.ToTable("Deliveries");

        builder.HasKey(d => d.Id).SxHidden();

        builder.Property(d => d.Id)
            .SxHidden();

        builder.Property(d => d.ScheduledDate)
            .IsRequired()
            .SxTitle("Data de entrega");

        builder.Property(d => d.Status)
            .IsRequired()
            .SxTitle("Status")
            .SXFixedValues(["Criado", "Em separação", "Em rota de entrega", "Entregue"]);

        builder.Property(d => d.CreationTime)
            .SxHidden()
            .IsRequired()
            .HasDefaultValue(DateTime.MinValue);

        builder.Property(d => d.LastModificationTime)
            .SxHidden();

        builder.Property(d => d.AddressId)
            .SxHidden();

        builder.HasMany(d => d.DeliveryItems)
            .WithOne(di => di.Delivery)
            .HasForeignKey(d => d.DeliveryId)
            .HasPrincipalKey(d => d.Id)
            .SxRelationTitle("Itens da entrega");
    }
}
