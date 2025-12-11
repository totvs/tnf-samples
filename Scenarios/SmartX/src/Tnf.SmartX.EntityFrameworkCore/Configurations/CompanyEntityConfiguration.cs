using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations;

public class CompanyEntityConfiguration : IEntityTypeConfiguration<CompanyEntity>, ISXEntityConfiguration
{
    public void Configure(EntityTypeBuilder<CompanyEntity> builder)
    {
        builder.ToTable("Companies");

        builder.SxObjectDescription("Cadastro de Empresas");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TenantId).IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName(nameof(CompanyEntity.Name))
            .SxTitle("Nome da Empresa")
            .SxDescription("Nome da Empresa");

        builder.Property(e => e.TradeName)
            .HasMaxLength(50)
            .HasColumnName(nameof(CompanyEntity.TradeName))
            .SxTitle("Nome Fatansia")
            .SxDescription("Nome Fantasia");

        builder.Property(e => e.RegistrationNumber)
            .HasMaxLength(50)
            .HasColumnName(nameof(CompanyEntity.RegistrationNumber))
            .SxTitle("CNPJ")
            .SxDescription("CNPJ");

        builder.Property(e => e.HasEsg)
            .SxTitle("Ignorar autenticação")
            .SxDescription("Ignorar autenticação")
            .HasDefaultValue(true)
            .SxBooleanLabelTrue("Sim")
            .SxBooleanLabelFalse("Não");

        builder.Property(e => e.Email)
            .HasColumnName(nameof(CompanyEntity.Email))
            .SxTitle("E-mail da Empresa")
            .SxDescription("E-mail da Empresa")
            .SxPattern("[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$");

        builder.Property(x => x.CreationTime)
            .IsRequired()
            .HasDefaultValue(DateTime.MinValue);

        builder.HasMany(e => e.Departments)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .HasPrincipalKey(e => e.Id)
            .SxRelationTitle("Departamentos");
    }
}
