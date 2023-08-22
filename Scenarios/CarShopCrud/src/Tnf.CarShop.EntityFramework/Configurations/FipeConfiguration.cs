using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.EntityFrameworkCore.Configurations;

public class FipeConfiguration : IEntityTypeConfiguration<Fipe>
{
    public void Configure(EntityTypeBuilder<Fipe> builder)
    {
        builder.ToTable("Fipe");

        builder.HasKey(x => x.Id);
    }
}
