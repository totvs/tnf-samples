using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Infra.Pocos;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.Sales.Infra
{
    public static class TnfConfigurationExtensions
    {
        public static void ConfigureInfra(this ITnfConfiguration tnfConfiguration)
        {
            tnfConfiguration.Repository(config =>
            {
                config.Entity<PurchaseOrderPoco>(entity => entity.RequestDto<DefaultRequestDto>((e, d) => e.Id == d.Id));
            });
        }
    }
}
