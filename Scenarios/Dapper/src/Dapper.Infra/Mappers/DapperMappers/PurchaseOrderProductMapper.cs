using Dapper.Infra.Entities;
using DapperExtensions.Mapper;

namespace Dapper.Infra.Mappers.DapperMappers
{
    public sealed class PurchaseOrderProductMapper : ClassMapper<PurchaseOrderProduct>
    {
        public PurchaseOrderProductMapper()
        {
            Table("PurchaseOrderProducts");
            Map(x => x.Id).Key(KeyType.Identity);
            Map(x => x.Product).Ignore();
            Map(x => x.PurchaseOrder).Ignore();
            AutoMap();
        }
    }
}
