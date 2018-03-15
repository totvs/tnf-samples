using BasicCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicCrud.Infra.Oracle.Context.Builders
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Value).IsRequired();
        }
    }
}
