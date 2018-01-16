using Case2.Infra.Context.Migration;
using Microsoft.EntityFrameworkCore.Migrations;
using Tnf.Settings.EntityFrameworkCore;

namespace Case2.Infra.Migrations.SettingDb
{
    /// <summary>
    /// O Tnf possui um helper para settings chamado TnfSettingDbContextSeedHelper que pode ser configurado
    /// na migração para criar as settings defaults do Framework (Utilizado abaixo).
    /// </summary>
    public partial class SeedSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var designFactory = new SettingDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                // Inclui as configurações a nível de aplicação por isso não é passado o TenantID
                TnfSettingDbContextSeedHelper.CreateDefaultSettings(
                    db,
                    null);

                db.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var designFactory = new SettingDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                TnfSettingDbContextSeedHelper.RemoveDefaultSettings(
                    db,
                    null);

                db.SaveChanges();
            }
        }
    }
}
