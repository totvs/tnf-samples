using Case2.Infra.Context.Migration;
using Microsoft.EntityFrameworkCore.Migrations;
using Tnf.Localization.EntityFrameworkCore;
using Tnf.Runtime.MultiTenancy;

namespace Case2.Infra.Migrations.LocalizationDb
{
    /// <summary>
    /// O Tnf possui um helper para localização chamado TnfLocalizationDbContextSeedHelper que pode ser configurado
    /// na migração para criar as keys de localização defaults do Framework (Utilizado abaixo).
    /// </summary>
    public partial class SeedLocalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var designFactory = new LocalizationDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                TnfLocalizationDbContextSeedHelper.CreateDefaultLocalizationValues(
                    db,
                    MultiTenancyConsts.DefaultTenantId);

                db.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var designFactory = new LocalizationDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                TnfLocalizationDbContextSeedHelper.RemoveDefaultLocalizationValues(
                    db,
                    MultiTenancyConsts.DefaultTenantId);

                db.SaveChanges();
            }
        }
    }
}
