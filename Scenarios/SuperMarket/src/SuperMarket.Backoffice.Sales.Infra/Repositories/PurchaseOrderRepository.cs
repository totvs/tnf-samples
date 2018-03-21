using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using SuperMarket.Backoffice.Sales.Infra.Contexts;
using SuperMarket.Backoffice.Sales.Infra.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.Sales.Infra.Repositories
{
    public class PurchaseOrderRepository : EfCoreRepositoryBase<SalesContext, PurchaseOrderPoco, Guid>, IPurchaseOrderRepository
    {
        private readonly INotificationHandler _notificationHandler;

        public PurchaseOrderRepository(
            IDbContextProvider<SalesContext> dbContextProvider, 
            INotificationHandler notificationHandler)
            : base(dbContextProvider)
        {
            _notificationHandler = notificationHandler;
        }

        public async Task<PurchaseOrder> GetPurchaseOrder(Guid id)
        {
            var poco = await GetAsync(id);

            // Load relationship
            await EnsureCollectionLoadedAsync(poco, i => i.PurchaseOrderProducts);

            var purchaseOrder = new PurchaseOrder
            {
                BaseValue = poco.BaseValue,
                CustomerId = poco.CustomerId,
                Date = poco.Date,
                Discount = poco.Discount,
                Id = poco.Id,
                Number = poco.Number,
                Status = poco.Status,
                Tax = poco.Tax,
                TotalValue = poco.TotalValue
            };

            foreach (var product in poco.PurchaseOrderProducts)
            {
                purchaseOrder.Lines.Add(new PurchaseOrder.PurchaseOrderLine(product.ProductId, product.Quantity));
            }

            return purchaseOrder;
        }

        public async Task<PurchaseOrder> Save(PurchaseOrder purchaseOrder)
        {
            var poco = await GetAsync(purchaseOrder.Id);

            // Load relationship
            await EnsureCollectionLoadedAsync(poco, i => i.PurchaseOrderProducts);

            var isNew = false;

            if (poco == null)
            {
                isNew = true;
                poco = new PurchaseOrderPoco();
            }

            poco.BaseValue = purchaseOrder.BaseValue;
            poco.CustomerId = purchaseOrder.CustomerId;
            poco.Date = purchaseOrder.Date;
            poco.Discount = purchaseOrder.Discount;
            poco.Id = purchaseOrder.Id;
            poco.Number = purchaseOrder.Number;
            poco.Status = purchaseOrder.Status;
            poco.Tax = purchaseOrder.Tax;
            poco.TotalValue = purchaseOrder.TotalValue;

            // Remove items
            var productsIds = purchaseOrder.Lines.Select(s => s.ProductId);

            Context.PurchaseOrderProducts.RemoveAll(w => !productsIds.Contains(w.ProductId));

            // Add or Update when item exist
            foreach (var line in purchaseOrder.Lines)
            {
                var purchaseOrderProduct = poco.PurchaseOrderProducts
                    .FirstOrDefault(w => w.ProductId == line.ProductId);

                if (purchaseOrderProduct == null)
                {
                    purchaseOrderProduct = new PurchaseOrderProductPoco();
                    poco.PurchaseOrderProducts.Add(purchaseOrderProduct);
                }

                purchaseOrderProduct.Quantity = line.Quantity;
                purchaseOrderProduct.UnitValue = purchaseOrder.PriceTable.GetPrice(line.ProductId);
            }

            if (isNew)
                await InsertAsync(poco);
            else
                await UpdateAsync(poco);

            return purchaseOrder;
        }
    }
}
