using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.FiscalService.Domain.Entities;

namespace SuperMarket.FiscalService.Infra
{
    public class PurchaseOrderTaxMovimentMapper : IEntityTypeConfiguration<TaxMoviment>
    {
        public void Configure(EntityTypeBuilder<TaxMoviment> builder)
        {
            builder.ToTable("PurchaseOrderTaxMoviments");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();

            builder.Property(p => p.PurchaseOrderTotalValue).HasColumnType("decimal(18, 6)").IsRequired();
            builder.Property(p => p.Percentage).IsRequired();
            builder.Property(p => p.PurchaseOrderBaseValue).HasColumnType("decimal(18, 6)").IsRequired();
            builder.Property(p => p.PurchaseOrderDiscount).HasColumnType("decimal(18, 6)").IsRequired();
            builder.Property(p => p.PurchaseOrderId).IsRequired();
            builder.Property(p => p.Tax).HasColumnType("decimal(18, 6)").IsRequired();
        }
    }
}
