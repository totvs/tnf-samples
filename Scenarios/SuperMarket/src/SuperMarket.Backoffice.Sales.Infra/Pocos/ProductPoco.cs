using System;
using System.Collections.Generic;
using Tnf.Repositories.Entities;

namespace SuperMarket.Backoffice.Sales.Infra.Pocos
{
    public class ProductPoco : Entity<Guid>
    {
        public ProductPoco()
        {
            PurchaseOrderProducts = new List<PurchaseOrderProductPoco>();
        }

        public string Description { get; set; }
        public decimal Value { get; set; }

        public ICollection<PurchaseOrderProductPoco> PurchaseOrderProducts { get; set; }
    }
}
