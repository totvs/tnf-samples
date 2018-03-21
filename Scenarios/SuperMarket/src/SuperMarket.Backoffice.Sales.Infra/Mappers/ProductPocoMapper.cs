using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Backoffice.Sales.Infra.Pocos;

namespace SuperMarket.Backoffice.Sales.Infra.Mappers
{
    public class ProductPocoMapper : IEntityTypeConfiguration<ProductPoco>
    {
        public void Configure(EntityTypeBuilder<ProductPoco> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Value).IsRequired();
        }
    }
}
