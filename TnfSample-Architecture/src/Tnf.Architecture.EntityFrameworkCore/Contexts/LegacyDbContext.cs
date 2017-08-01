using Microsoft.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Contexts.Mappers;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts
{
    public class LegacyDbContext : TnfDbContext
    {
        public DbSet<ProfessionalPoco> Professionals { get; set; }
        public DbSet<ProfessionalSpecialtiesPoco> ProfessionalSpecialties { get; set; }
        public DbSet<SpecialtyPoco> Specialties { get; set; }

        public LegacyDbContext(DbContextOptions<LegacyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProfessionalPocoMap.Map(modelBuilder);
            SpecialtyPocoMap.Map(modelBuilder);
            ProfessionalSpecialtiesPocoMap.Map(modelBuilder);
        }
    }
}
