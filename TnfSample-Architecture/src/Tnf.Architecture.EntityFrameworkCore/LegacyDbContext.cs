using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.EntityFrameworkCore
{
    public class LegacyDbContext : TnfDbContext
    {
        public DbSet<ProfessionalPoco> Professionals { get; set; }
        public DbSet<ProfessionalSpecialtiesPoco> ProfessionalSpecialties { get; set; }
        public DbSet<SpecialtyPoco> Specialties { get; set; }

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

            modelBuilder.Entity<SpecialtyPoco>(m =>
            {
                // PKs
                m.HasKey(i => i.Id);

                // Configure PKs auto generated
                m.Property(i => i.Id).ValueGeneratedOnAdd().HasColumnName("SYS011_SPECIALTIES_ID");

                m.Property(p => p.Description).HasColumnName("SYS011_SPECIALTIES_DESCRIPTION").HasMaxLength(100).IsRequired();
                m.ToTable("SYS011_SPECIALTIES");
            });

            modelBuilder.Entity<ProfessionalSpecialtiesPoco>(m =>
            {
                // PKs
                m.Ignore(i => i.Id);
                m.HasKey(i => i.SpecialtyId);
                m.HasKey(i => new { i.ProfessionalId, i.Code });
                
                m.Property(i => i.ProfessionalId).HasColumnName("SYS009_PROFESSIONAL_ID");
                m.Property(i => i.Code).HasColumnName("SYS009_PROFESSIONAL_CODE");
                m.Property(i => i.SpecialtyId).HasColumnName("SYS011_SPECIALTIES_ID");

                m.HasOne(o => o.Professional)
                    .WithMany(w => w.ProfessionalSpecialties)
                    .HasPrincipalKey(k => new { k.ProfessionalId, k.Code })
                    .HasForeignKey(k => new { k.ProfessionalId, k.Code });

                m.HasOne(o => o.Specialty)
                    .WithMany(w => w.ProfessionalSpecialties)
                    .HasForeignKey(k => k.SpecialtyId);
                m.ToTable("SYS010_PROFESSIONAL_SPECIALTIES");
            });
        }
    }
}
