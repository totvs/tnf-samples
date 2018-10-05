using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Infra.Contexts;
using SuperMarket.Backoffice.Sales.Infra.Pocos;
using SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace SuperMarket.Backoffice.Sales.Infra.Repositories
{
    public class PurchaseOrderReadRepository : EfCoreRepositoryBase<SalesContext, PurchaseOrderPoco>, IPurchaseOrderReadRepository
    {
        public PurchaseOrderReadRepository(IDbContextProvider<SalesContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<IListDto<PurchaseOrderDto>> GetAllPurchaseOrdersAsync(PurchaseOrderRequestAllDto request)
        {
            return GetAllAsync<PurchaseOrderDto>(request, p => FilterPurchase(p, request));
        }

        private bool FilterPurchase(PurchaseOrderPoco purchaseOrder, PurchaseOrderRequestAllDto request)
        {
            var validateNumber = false;
            var validateDate = false;

            validateNumber = (request.Number == null || request.Number == Guid.Empty || request.Number == purchaseOrder.Number);

            if (request.StartDate == DateTime.MinValue || request.StartDate == null)
                return validateNumber;

            if (request.EndDate == DateTime.MinValue || request.EndDate == null)
                validateDate = request.StartDate == purchaseOrder.Date;
            else
                validateDate = (purchaseOrder.Date >= request.StartDate.Value) && (purchaseOrder.Date <= request.EndDate.Value);

            return validateNumber && validateDate;
        }

        public async Task<PurchaseOrderDto> GetPurchaseOrderAsync(DefaultRequestDto key)
        {
            var entity = await GetAsync(key);

            return entity.MapTo<PurchaseOrderDto>();
        }
    }
}
