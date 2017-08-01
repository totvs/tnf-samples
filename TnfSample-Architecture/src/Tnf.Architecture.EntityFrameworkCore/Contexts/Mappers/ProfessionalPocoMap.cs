using Microsoft.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts.Mappers
{
    public class ProfessionalPocoMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessionalPoco>(m =>
            {
                // PKs
                m.Ignore(i => i.Id);
                m.HasKey(i => new { i.ProfessionalId, i.Code });

                // Configure PKs auto generated
                m.Property(i => i.Code).ValueGeneratedOnAdd().HasColumnName("SYS009_PROFESSIONAL_CODE");
                // Hack pois o método ValueGeneratedOnAdd do EF Core ainda não aceita propriedades decimal
                m.Property(i => i.ProfessionalId).HasDefaultValueSql("SELECT ISNULL(MAX(SYS009_PROFESSIONAL_ID), 1) FROM SYS009_PROFESSIONAL").HasColumnName("SYS009_PROFESSIONAL_ID");

                m.Property(p => p.Name).HasColumnName("SYS009_NAME").HasMaxLength(50).IsRequired();
                m.Property(p => p.Address).HasColumnName("SYS009_ADDRESS").HasMaxLength(50).IsRequired();
                m.Property(p => p.AddressComplement).HasColumnName("SYS009_ADDRESS_COMPLEMENT").HasMaxLength(100).IsRequired();
                m.Property(p => p.AddressNumber).HasColumnName("SYS009_ADDRESS_NUMBER").HasMaxLength(9).IsRequired();
                m.Property(p => p.Email).HasColumnName("SYS009_EMAIL").HasMaxLength(50).IsRequired();
                m.Property(p => p.Email).HasColumnName("SYS009_EMAIL").HasMaxLength(50).IsRequired();
                m.Property(p => p.Phone).HasColumnName("SYS009_PHONE").HasMaxLength(50).IsRequired();
                m.Property(p => p.ZipCode).HasColumnName("SYS009_ZIP_CODE").HasMaxLength(15).IsRequired();

                m.ToTable("SYS009_PROFESSIONAL");
            });
        }
    }
}
