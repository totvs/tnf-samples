using System;
using System.Collections.Generic;
using Tnf.Repositories.Entities;

namespace Transactional.Domain.Entities
{
    public class Order : Entity
    {
        public DateTime Data { get; set; }
        public int ClientId { get; set; }
        public decimal BaseValue { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public int Tax { get; set; }
        public ICollection<ProductOrder> Products { get; set; } = new List<ProductOrder>();
    }
}
