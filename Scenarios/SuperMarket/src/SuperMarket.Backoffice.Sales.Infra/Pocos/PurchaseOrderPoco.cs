using System;
using System.Collections.Generic;
using Tnf.Repositories.Entities;
using static SuperMarket.Backoffice.Sales.Domain.Entities.PurchaseOrder;

namespace SuperMarket.Backoffice.Sales.Infra.Pocos
{
    public class PurchaseOrderPoco : Entity<Guid>
    {
        public PurchaseOrderPoco()
        {
            PurchaseOrderProducts = new List<PurchaseOrderProductPoco>();
        }

        public Guid Number { get; set; }
        public DateTime Date { get; set; }
        public decimal? TotalValue { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Discount { get; set; }
        public decimal? Tax { get; set; }
        public decimal BaseValue { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public ICollection<PurchaseOrderProductPoco> PurchaseOrderProducts { get; set; }
    }
}
