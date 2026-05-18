using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TnfZero.Domain.Entities;

namespace TnfZero.EntityFramework.Configurations;

public class DogEntityConfiguration : IEntityTypeConfiguration<DogEntity>
{
    public void Configure(EntityTypeBuilder<DogEntity> builder)
    {
        builder.ToTable("Dogs");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName(nameof(DogEntity.Name));
    }
}