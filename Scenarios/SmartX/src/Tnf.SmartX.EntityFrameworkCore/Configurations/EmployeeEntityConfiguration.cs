using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations
{
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<EmployeeEntity>, ISXEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable("Employees")
                .SxObjectDescription("Colaboradores")
                .SxFinder(nameof(EmployeeEntity.FullName), nameof(EmployeeEntity.EmailAddress));

            builder.HasKey(e => e.Id).SxHidden();

            builder.Property(x => x.TeamId).SxHidden();

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100)
                .SxTitle("Nome do Colaborador");

            builder.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(50)
                .SxTitle("E-mail do Colaborador")
                .SxPattern("[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$");

            builder.Property(e => e.Position)
                .IsRequired()
                .SxTitle("Cargo do Colaborador")
                .SXFixedValues(["Analista", "Especialista", "Coordenador", "Gerente Executivo"]);
        }
    }
}
