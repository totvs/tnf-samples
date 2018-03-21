using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Backoffice.FiscalService.Domain.Entities;

namespace SuperMarket.Backoffice.FiscalService.Infra
{
    public class PurchaseOrderTaxMovimentMapper : IEntityTypeConfiguration<PurchaseOrderTaxMoviment>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderTaxMoviment> builder)
        {
            builder.ToTable("PurchaseOrderTaxMoviments");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();

            builder.Property(p => p.PurchaseOrderNumber).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.CustomerId).IsRequired();
            builder.Property(p => p.Percentage).IsRequired();
            builder.Property(p => p.BaseValue).HasColumnType("decimal(18, 6)").IsRequired();
            builder.Property(p => p.TotalValue).HasColumnType("decimal(18, 6)").IsRequired();
        }
    }
}
