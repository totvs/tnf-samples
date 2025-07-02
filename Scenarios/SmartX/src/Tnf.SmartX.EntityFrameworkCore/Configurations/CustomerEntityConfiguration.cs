using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations;

public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>, ISXEntityConfiguration
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired();

        builder.Property(c => c.Email)
            .IsRequired()
            .SxPattern("[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$");

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .SxPattern("^(\\(\\d{2}\\)\\s9\\d{4}-\\d{4}|\\d{2}9\\d{8})$");

        builder.Property(c => c.CreationTime)
            .IsRequired()
            .HasDefaultValue(DateTime.MinValue);

        builder.HasMany(c => c.Addresses)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .HasPrincipalKey(c => c.Id)
            .SxRelationTitle("Endereços");
    }
}
