using System;
using System.Collections.Generic;
using Tnf.Repositories.Entities;

namespace Dapper.Infra.Entities
{
    public class PurchaseOrder : Entity
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalValue { get; set; }

        public Customer Customer { get; set; }
        public ICollection<PurchaseOrderProduct> PurchaseOrderProducts { get; set; } = new List<PurchaseOrderProduct>();

        public PurchaseOrder() { }

        public PurchaseOrder(DateTime date, int customerId, decimal totalValue)
        {
            Date = date;
            CustomerId = customerId;
            TotalValue = totalValue;
        }
    }
}
