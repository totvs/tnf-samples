namespace Transactional.Domain.Entities
{
    public class PurchaseOrderProduct : IEntity
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitValue { get; set; }
        public int Amount { get; set; }
    }
}
