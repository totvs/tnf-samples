using BasicCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicCrud.Infra.SqLite.Context.Builders
{
    public class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
