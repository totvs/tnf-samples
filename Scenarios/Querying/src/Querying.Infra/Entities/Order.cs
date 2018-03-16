using System;
using System.Collections.Generic;
using Tnf.Repositories.Entities;

namespace Querying.Infra.Entities
{
    public class Order : Entity
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalValue { get; set; }

        public Customer Customer { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
