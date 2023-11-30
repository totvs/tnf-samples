using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.EntityFrameworkCore.Configurations;

public class FipeConfiguration : IEntityTypeConfiguration<Fipe>
{
    public void Configure(EntityTypeBuilder<Fipe> builder)
    {
        builder.ToTable("Fipe");

        builder.Property(x => x.FipeCode).IsRequired();
        builder.Property(x => x.MonthYearReference).IsRequired();
        builder.Property(x => x.Brand).IsRequired();
        builder.Property(x => x.Model).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
