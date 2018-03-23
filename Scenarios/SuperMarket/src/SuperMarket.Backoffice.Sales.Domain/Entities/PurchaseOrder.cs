using System;
using System.Collections.Generic;
using System.Linq;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace SuperMarket.Backoffice.Sales.Domain.Entities
{
    public partial class PurchaseOrder : Entity<Guid>
    {
        public static INewPurchaseOrderBuilder New(INotificationHandler notificationHandler)
            => new Builder(notificationHandler)
            .GenerateNewPurchaseOrder();

        public static IUpdatePurchaseOrderBuilder Update(INotificationHandler notificationHandler, PurchaseOrder purchaseOrder)
            => new Builder(notificationHandler, purchaseOrder);

        public Guid Number { get; private set; }
        public DateTime Date { get; private set; }
        public decimal TotalValue { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Tax { get; private set; }
        public decimal BaseValue { get; private set; }
        public PurchaseOrderStatus Status { get; private set; }
        public ICollection<PurchaseOrderLine> Lines { get; private set; } = new List<PurchaseOrderLine>();

        private PriceTable PriceTable { get; set; } = PriceTable.Empty();

        internal IEnumerable<Guid> GetProductsThatAreNotInThePriceTable()
        {
            var products = Lines.Select(s => s.ProductId);

            return products.Where(a => !PriceTable.ConstainsPrice(a));
        }

        internal IEnumerable<Guid> GetProductsWhenHaveNegativeQuantity()
        {
            var lines = Lines.Where(w => w.Quantity <= 0);

            return lines.Select(s => s.ProductId);
        }

        internal void RecalculateBaseValue()
        {
            BaseValue = 0;

            foreach (var line in Lines)
            {
                if (PriceTable.ConstainsPrice(line.ProductId))
                {
                    var price = PriceTable.GetPrice(line.ProductId);

                    BaseValue += price * line.Quantity;
                }
            }
        }

        public decimal GetProductPrice(Guid productId) => PriceTable.GetPrice(productId);

        public void UpdateTaxMoviment(decimal tax, decimal totalValue)
        {
            Tax = tax;
            TotalValue = totalValue;

            Status = PurchaseOrderStatus.Completed;
        }

        public class PurchaseOrderLine
        {
            public Guid ProductId { get; }
            public int Quantity { get; }

            public PurchaseOrderLine(Guid productId, int quantity)
            {
                ProductId = productId;
                Quantity = quantity;
            }
        }
    }
}
