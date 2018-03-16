using Tnf.Repositories.Entities;

namespace Querying.Infra.Entities
{
    public class ProductOrder : Entity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitValue { get; set; }
        public int Amount { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
