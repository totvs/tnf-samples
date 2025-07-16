using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.EntityFramework.Configurations
{
    public class TeamEntityConfiguration : IEntityTypeConfiguration<TeamEntity>, ISXEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<TeamEntity> builder)
        {
            builder.ToTable("Teams");

            builder.SxObjectDescription("Equipes do Departamento")
                .SxFinder(nameof(TeamEntity.Name));

            builder.HasKey(e => e.Id).SxHidden();

            builder.Property(x => x.DepartmentId).SxHidden();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20)
                .SxTitle("Nome do Time");

            builder.HasMany(e => e.Employees)
                .WithOne(e => e.Team)
                .HasForeignKey(e => e.TeamId)
                .HasPrincipalKey(e => e.Id)
                .SxRelationTitle("Colaboradores");
        }
    }
}
