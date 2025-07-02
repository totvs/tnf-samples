using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations;

public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>, ISXEntityConfiguration
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Street)
            .SxTitle("Rua")
            .IsRequired();

        builder.Property(a => a.City)
            .SxTitle("Cidade")
            .IsRequired();

        builder.Property(a => a.ZipCode)
            .IsRequired()
            .SxTitle("CEP")
            .SxPattern("^\\d{8}$");

        builder.Property(a => a.Id).SxHidden();


        builder.Property(a => a.CreationTime)
            .SxHidden()
            .IsRequired()
            .HasDefaultValue(DateTime.MinValue);

        builder.Property(a => a.LastModificationTime)
            .SxHidden();

        builder.Property(a => a.CustomerId)
            .SxHidden();

        builder.Property(a => a.State)
            .SxTitle("Estado");

        builder.HasMany(a => a.Deliveries)
            .WithOne(d => d.Address)
            .HasForeignKey(a => a.AddressId)
            .HasPrincipalKey(a => a.Id)
            .SxRelationTitle("Entregas");
    }
}
