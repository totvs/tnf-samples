using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Backoffice.Sales.Infra.Pocos;

namespace SuperMarket.Backoffice.Sales.Infra.Mappers
{
    public class PurchaseOrderProductPocoMapper : IEntityTypeConfiguration<PurchaseOrderProductPoco>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderProductPoco> builder)
        {
            builder.ToTable("PurchaseOrderProducts");

            builder.HasKey(p => new { p.ProductId, p.PurchaseOrderId });

            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.PurchaseOrderId).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.UnitValue).HasColumnType("decimal(18, 6)").IsRequired();

            builder
                .HasOne(pt => pt.PurchaseOrder)
                .WithMany(p => p.PurchaseOrderProducts)
                .HasForeignKey(p => p.PurchaseOrderId);
        }
    }
}
