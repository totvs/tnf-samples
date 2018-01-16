using Microsoft.EntityFrameworkCore;
using Tnf.Localization.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Case2.Infra.Context
{
    /// <summary>
    /// Para utilizar a localização via banco de dados é preciso implementar um DbContext que irá fazer a herança da classe
    /// TnfLocalizationDbContext. Dessa forma poderá ser inclusa a migração na aplicação
    /// </summary>
    public class LocalizationDbContext : TnfLocalizationDbContext<LocalizationDbContext>
    {
        // Importante o construtor do contexto receber as opções com o tipo generico definido: DbContextOptions<TDbContext>
        public LocalizationDbContext(DbContextOptions<LocalizationDbContext> options, ITnfSession tnfSession)
            : base(options, tnfSession)
        {
        }
    }
}
