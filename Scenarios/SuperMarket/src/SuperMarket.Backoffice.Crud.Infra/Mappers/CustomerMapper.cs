using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Backoffice.Crud.Domain.Entities;

namespace SuperMarket.Backoffice.Crud.Infra.Mappers
{
    public class CustomerMapper : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
