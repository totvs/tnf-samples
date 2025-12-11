using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using Transactional.Domain.Entities;
using Transactional.Domain.Interfaces;
using Transactional.Infra.Context;

namespace Transactional.Infra.Repositories
{
    public class PurchaseOrderRepository : EfCoreRepositoryBase<PurchaseOrderContext, PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(IDbContextProvider<PurchaseOrderContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<PurchaseOrder> CheckDuplicatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            var duplicateOrder = await FirstOrDefaultAsync(w => w.ClientId == purchaseOrder.ClientId && w.Data == purchaseOrder.Data);
            return duplicateOrder;
        }

        public async Task<PurchaseOrder> CreateNewPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            purchaseOrder = await base.InsertAndSaveChangesAsync(purchaseOrder);
            return purchaseOrder;
        }

        public Task DeleteAsync(int id) => DeleteAsync(w => w.Id == id);

        public List<PurchaseOrder> GetAllPurchaseOrders()
        {
            return base.GetAllIncluding(i => i.PurchaseOrderProducts).ToList();
        }
    }
}
