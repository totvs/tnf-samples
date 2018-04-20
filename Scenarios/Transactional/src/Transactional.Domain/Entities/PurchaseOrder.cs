using System;
using System.Collections.Generic;

namespace Transactional.Domain.Entities
{
    public class PurchaseOrder : IEntity
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int ClientId { get; set; }
        public decimal BaseValue { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public int Tax { get; set; }
        public ICollection<PurchaseOrderProduct> PurchaseOrderProducts { get; set; } = new List<PurchaseOrderProduct>();
    }
}
