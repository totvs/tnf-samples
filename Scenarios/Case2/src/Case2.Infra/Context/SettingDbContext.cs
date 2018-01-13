using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;
using Tnf.Settings.EntityFrameworkCore;

namespace Case2.Infra.Context
{
    /// <summary>
    /// Para utilizar as settings via banco de dados é preciso implementar um DbContext que irá fazer a herança da classe
    /// TnfSettingsDbContext. Dessa forma poderá ser inclusa a migração na aplicação
    /// </summary>
    public class SettingDbContext : TnfSettingsDbContext<SettingDbContext>
    {
        public SettingDbContext(DbContextOptions<SettingDbContext> options, ITnfSession tnfSession)
            : base(options, tnfSession)
        {
        }
    }
}
