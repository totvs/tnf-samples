using Dapper.Infra.Entities;
using DapperExtensions.Mapper;

namespace Dapper.Infra.Mappers.DapperMappers
{
    public sealed class ProductMapper : ClassMapper<Product>
    {
        public ProductMapper()
        {
            Table("Products");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}
