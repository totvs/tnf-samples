using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Infra.Pocos;

namespace SuperMarket.Backoffice.Sales.Infra.Mappers
{
    public class PurchaseOrderPocoMapper : IEntityTypeConfiguration<PurchaseOrderPoco>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderPoco> builder)
        {
            builder.ToTable("PurchaseOrders");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.BaseValue).HasColumnType("decimal(18, 6)").IsRequired();
            builder.Property(p => p.CustomerId).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Discount).HasColumnType("decimal(18, 6)").IsRequired();
            builder.Property(p => p.Number).IsRequired();
            builder.Property(p => p.Status).HasDefaultValue(PurchaseOrder.PurchaseOrderStatus.Processing).IsRequired();
            builder.Property(p => p.Tax).HasColumnType("decimal(18, 6)");
            builder.Property(p => p.TotalValue).HasColumnType("decimal(18, 6)");
        }
    }
}
