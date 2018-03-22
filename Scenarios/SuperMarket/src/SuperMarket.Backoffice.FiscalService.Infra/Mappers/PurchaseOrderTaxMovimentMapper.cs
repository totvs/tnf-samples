using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Backoffice.FiscalService.Domain.Entities;

namespace SuperMarket.Backoffice.FiscalService.Infra
{
    public class PurchaseOrderTaxMovimentMapper : IEntityTypeConfiguration<TaxMoviment>
    {
        public void Configure(EntityTypeBuilder<TaxMoviment> builder)
        {
            builder.ToTable("PurchaseOrderTaxMoviments");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();

            builder.Property(p => p.OrderNumber).IsRequired();
            builder.Property(p => p.OrderDate).IsRequired();
            builder.Property(p => p.OrderCustomer).IsRequired();
            builder.Property(p => p.Percentage).IsRequired();
            builder.Property(p => p.OrderBaseValue).HasColumnType("decimal(18, 6)").IsRequired();
            builder.Property(p => p.OrderTotalValue).HasColumnType("decimal(18, 6)").IsRequired();
        }
    }
}
