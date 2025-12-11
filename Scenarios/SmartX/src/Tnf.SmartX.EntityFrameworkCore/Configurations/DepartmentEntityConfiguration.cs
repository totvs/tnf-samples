using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<DepartmentEntity>, ISXEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
            builder.ToTable("Departments");

            builder.SxObjectDescription("Cadastro de Departamentos")
                .SxFinder(nameof(DepartmentEntity.CompanyId));

            builder.HasKey(e => e.Id).SxHidden();

            builder.Property(e => e.CompanyId).SxHidden();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .SxTitle("Nome do Departamento");

            builder.Property(x => x.CreationTime)
                .IsRequired()
                .HasDefaultValue(DateTime.MinValue)
                .SxHidden();

            builder.Property(x => x.LastModificationTime)
                .SxHidden();

            builder.HasMany(e => e.Teams)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .HasPrincipalKey(e => e.Id)
                .SxRelationTitle("Equipes");
        }
    }
}
