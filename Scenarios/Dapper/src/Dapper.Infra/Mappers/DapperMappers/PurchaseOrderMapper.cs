using Dapper.Infra.Entities;
using DapperExtensions.Mapper;

namespace Dapper.Infra.Mappers.DapperMappers
{
    public sealed class PurchaseOrderMapper : ClassMapper<PurchaseOrder>
    {
        public PurchaseOrderMapper()
        {
            Table("PurchaseOrders");
            Map(x => x.Customer).Ignore();
            Map(x => x.PurchaseOrderProducts).Ignore();
            AutoMap();
        }
    }
}
