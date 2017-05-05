using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.EntityFrameworkCore
{
    public class LegacyDbContext : TnfDbContext
    {
        public DbSet<ProfessionalPoco> Professionals { get; set; }

        public LegacyDbContext(DbContextOptions<LegacyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessionalPoco>(m =>
            {
                // PKs
                m.Ignore(i => i.Id);
                m.HasKey(i => i.ProfessionalId);
                m.HasKey(i => i.Code);

                // Configure PKs auto generated
                m.Property(i => i.Code).ValueGeneratedOnAdd().HasColumnName("SYS009_PROFESSIONAL_CODE");
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
