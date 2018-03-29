using Dapper.Infra.Entities;
using DapperExtensions.Mapper;

namespace Dapper.Infra.Mappers.DapperMappers
{
    public sealed class CustomerMapper : ClassMapper<Customer>
    {
        public CustomerMapper()
        {
            Table("Customers");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}
