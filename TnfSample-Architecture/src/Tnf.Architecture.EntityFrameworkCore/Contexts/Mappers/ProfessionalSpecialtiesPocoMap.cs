using Microsoft.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts.Mappers
{
    public class ProfessionalSpecialtiesPocoMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessionalSpecialtiesPoco>(m =>
            {
                // PKs
                m.Ignore(i => i.Id);
                m.HasKey(i => new { i.ProfessionalId, i.Code, i.SpecialtyId });

                m.Property(i => i.ProfessionalId).HasColumnName("SYS009_PROFESSIONAL_ID");
                m.Property(i => i.Code).HasColumnName("SYS009_PROFESSIONAL_CODE");
                m.Property(i => i.SpecialtyId).HasColumnName("SYS011_SPECIALTIES_ID");

                m.HasOne(o => o.Professional)
                    .WithMany(w => w.ProfessionalSpecialties)
                    .HasPrincipalKey(k => new { k.ProfessionalId, k.Code })
                    .HasForeignKey(k => new { k.ProfessionalId, k.Code });

                m.HasOne(o => o.Specialty)
                    .WithMany(w => w.ProfessionalSpecialties)
                    .HasPrincipalKey(k => k.Id)
                    .HasForeignKey(k => k.SpecialtyId);

                m.ToTable("SYS010_PROFESSIONAL_SPECIALTIES");
            });
        }
    }
}
