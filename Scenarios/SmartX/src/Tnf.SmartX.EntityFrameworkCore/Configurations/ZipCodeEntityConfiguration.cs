using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations;

public class ZipCodeEntityConfiguration : IEntityTypeConfiguration<ZipCodeEntity>, ISXEntityConfiguration
{
    public void Configure(EntityTypeBuilder<ZipCodeEntity> builder)
    {
        builder.ToTable("ZipCodes");

        builder.HasKey(z => z.ZipCode);

        builder.Property(z => z.ZipCode)
            .SxMask("99999-999")
            .SxTitle("CEP");

        builder.Property(z => z.Street)
            .SxTitle("Rua")
            .IsRequired();

        builder.Property(z => z.City)
            .SxTitle("Cidade")
            .IsRequired();

        builder.Property(z => z.State)
            .SxTitle("Estado")
            .IsRequired();
    }
}
