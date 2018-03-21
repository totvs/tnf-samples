using System;
using System.Collections.Generic;
using System.Linq;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace SuperMarket.Backoffice.Sales.Domain.Entities
{
    public partial class PurchaseOrder : Entity<Guid>
    {
        public static void UpdateTax(PurchaseOrder purchaseOrder, decimal tax)
        {
            purchaseOrder.Tax = tax;

            purchaseOrder.TotalValue = (purchaseOrder.BaseValue - purchaseOrder.Discount) + purchaseOrder.Tax;

            purchaseOrder.Status = PurchaseOrderStatus.Completed;
        }

        public static INewPurchaseOrderBuilder New(INotificationHandler notificationHandler)
            => new PurchaseOrderBuilder(notificationHandler)
            .GenerateNewPurchaseOrder();

        public static IUpdatePurchaseOrderBuilder Update(INotificationHandler notificationHandler, PurchaseOrder purchaseOrder)
            => new PurchaseOrderBuilder(notificationHandler, purchaseOrder);

        public Guid Number { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalValue { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal BaseValue { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public ICollection<PurchaseOrderLine> Lines { get; set; } = Enumerable.Empty<PurchaseOrderLine>().ToList();
        public PriceTable PriceTable { get; set; } = PriceTable.Empty();

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

        private void RecalculateBaseValue()
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
