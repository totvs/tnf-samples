using System;
using Tnf.Repositories.Entities;

namespace SuperMarket.Backoffice.FiscalService.Domain.Entities
{
    public class PurchaseOrderTaxMoviment : Entity<Guid>
    {
        public Guid PurchaseOrderNumber { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public int Percentage { get; set; }
        public decimal BaseValue { get; set; }
        public decimal TotalValue { get; set; }
    }
}
