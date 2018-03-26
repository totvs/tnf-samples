using Microsoft.EntityFrameworkCore;
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
            var poco = await GetAll()
                .AsNoTracking()
                .Include(p => p.PurchaseOrderProducts)
                .SingleOrDefaultAsync(p => p.Id == id);

            return poco.MapTo<PurchaseOrder>();
        }

        public async Task<PurchaseOrder> Insert(PurchaseOrder purchaseOrder)
        {
            var poco = purchaseOrder.MapTo<PurchaseOrderPoco>();

            purchaseOrder.Id = await InsertAndGetIdAsync(poco);

            return purchaseOrder;
        }

        public async Task<PurchaseOrder> Update(PurchaseOrder purchaseOrder)
        {
            var poco = await GetAsync(purchaseOrder.Id);

            // Load relationship
            await EnsureCollectionLoadedAsync(poco, i => i.PurchaseOrderProducts);

            var pocoToUpdate = purchaseOrder.MapTo(poco);

            // Remove items
            var productsIds = purchaseOrder.Lines.Select(s => s.ProductId);

            Context.PurchaseOrderProducts.RemoveAll(w => !productsIds.Contains(w.ProductId));

            await UpdateAsync(pocoToUpdate);

            return purchaseOrder;
        }

        public async Task UpdateTaxMoviment(PurchaseOrder purchaseOrder)
        {
            var poco = await GetAsync(purchaseOrder.Id);

            poco.Tax = purchaseOrder.Tax;
            poco.TotalValue = purchaseOrder.TotalValue;
            poco.Status = purchaseOrder.Status;

            await UpdateAsync(poco);
        }
    }
}
