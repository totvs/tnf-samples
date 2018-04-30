using Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Security.Infra.Context.Builders
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Value).IsRequired();
        }
    }
}
