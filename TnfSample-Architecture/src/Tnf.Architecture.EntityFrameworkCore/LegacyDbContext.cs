using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Architecture.Domain.Registration;

namespace Tnf.Architecture.EntityFrameworkCore
{
    public class LegacyDbContext : TnfDbContext
    {
        public DbSet<Professional> Professionals { get; set; }

        public LegacyDbContext(DbContextOptions<LegacyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professional>()
                .Property(p => p.Name).HasColumnName("SYS009_NAME").HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Professional>()
                .Property(p => p.Address).HasColumnName("SYS009_ADDRESS").HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Professional>()
                .Property(p => p.AddressComplement).HasColumnName("SYS009_ADDRESS_COMPLEMENT").HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Professional>()
                .Property(p => p.AddressNumber).HasColumnName("SYS009_ADDRESS_NUMBER").HasMaxLength(9).IsRequired();

            modelBuilder.Entity<Professional>()
                .Property(p => p.Email).HasColumnName("SYS009_EMAIL").HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Professional>()
                .Property(p => p.Phone).HasColumnName("SYS009_PHONE").HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Professional>()
                .Property(p => p.ZipCode).HasColumnName("SYS009_ZIP_CODE").HasMaxLength(15).IsRequired();

            modelBuilder.Entity<Professional>().Ignore(i => i.Id);

            modelBuilder.Entity<Professional>()
                .Property(i => i.ProfessionalId)
                .HasColumnName("SYS009_PROFESSIONAL_ID");

            modelBuilder.Entity<Professional>()
                .HasKey(i => i.Code);

            modelBuilder.Entity<Professional>()
                .Property(i => i.Code)
                .HasColumnName("SYS009_PROFESSIONAL_CODE");

            modelBuilder.Entity<Professional>().ToTable("SYS009_PROFESSIONAL");
        }
    }
}
