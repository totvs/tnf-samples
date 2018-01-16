using Case2.Infra.Context.Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;
using Tnf.Localization.Management;
using Tnf.Runtime.MultiTenancy;

namespace Case2.Infra.Migrations.LocalizationDb
{
    public partial class AddLocalizationKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var designFactory = new LocalizationDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                db.LanguageTexts.Add(new TnfLanguageText()
                {
                    CreationTime = DateTime.UtcNow,
                    Key = LocalizationKeys.CustomMessage.ToString(),
                    LanguageName = "pt-BR",
                    Source = InfraConsts.LocalizationSourceName,
                    TenantId = MultiTenancyConsts.DefaultTenantId,
                    Value = "Mensagem customizada"
                });

                db.LanguageTexts.Add(new TnfLanguageText()
                {
                    CreationTime = DateTime.UtcNow,
                    Key = LocalizationKeys.CustomMessage.ToString(),
                    LanguageName = "en",
                    Source = InfraConsts.LocalizationSourceName,
                    TenantId = MultiTenancyConsts.DefaultTenantId,
                    Value = "Custom message"
                });

                db.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var designFactory = new LocalizationDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                // Importante utilizar na migração a extensão IgnoreQueryFilters para que os filtros do
                // dbcontext não sejam aplicados. Caso contrario a migração dará erro ao ser executada
                var keys = db.LanguageTexts.IgnoreQueryFilters()
                    .Where(w => w.Key == LocalizationKeys.CustomMessage.ToString()).ToArray();

                db.LanguageTexts.RemoveRange(keys);

                db.SaveChanges();
            }
        }
    }
}
