using Tnf.Repositories.Entities;

namespace Transactional.Domain.Entities
{
    public class PurchaseOrderProduct : Entity
    {
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitValue { get; set; }
        public int Amount { get; set; }
    }
}
