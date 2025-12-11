using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations;

public class StateEntityConfiguration : IEntityTypeConfiguration<StateEntity>, ISXEntityConfiguration
{
    public void Configure(EntityTypeBuilder<StateEntity> builder)
    {
        builder.ToTable("States");

        builder.HasKey(s => s.UF);

        builder.Property(s => s.Description)
            .SxTitle("Estado");

        builder.Property(s => s.UF)
            .SxTitle("UF");
    }
}
