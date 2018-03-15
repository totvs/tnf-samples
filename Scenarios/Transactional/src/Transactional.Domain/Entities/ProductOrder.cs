using Tnf.Repositories.Entities;

namespace Transactional.Domain.Entities
{
    public class ProductOrder : Entity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitValue { get; set; }
        public int Amount { get; set; }
    }
}
