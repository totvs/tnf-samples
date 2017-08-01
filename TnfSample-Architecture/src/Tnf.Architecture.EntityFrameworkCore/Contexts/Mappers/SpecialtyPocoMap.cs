using Microsoft.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts.Mappers
{
    public class SpecialtyPocoMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpecialtyPoco>(m =>
            {
                // PKs
                m.HasKey(i => i.Id);

                // Configure PKs auto generated
                m.Property(i => i.Id).ValueGeneratedOnAdd().HasColumnName("SYS011_SPECIALTIES_ID");

                m.Property(p => p.Description).HasColumnName("SYS011_SPECIALTIES_DESCRIPTION").HasMaxLength(100).IsRequired();

                m.ToTable("SYS011_SPECIALTIES");
            });
        }
    }
}
