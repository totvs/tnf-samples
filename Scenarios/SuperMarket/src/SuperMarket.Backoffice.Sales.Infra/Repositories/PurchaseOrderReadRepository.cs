using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Infra.Contexts;
using SuperMarket.Backoffice.Sales.Infra.Pocos;
using SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace SuperMarket.Backoffice.Sales.Infra.Repositories
{
    public class PurchaseOrderReadRepository : EfCoreRepositoryBase<SalesContext, PurchaseOrderPoco, Guid>, IPurchaseOrderReadRepository
    {
        public PurchaseOrderReadRepository(IDbContextProvider<SalesContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public Task<IListDto<PurchaseOrderDto, Guid>> GetAllPurchaseOrdersAsync(PurchaseOrderRequestAllDto request)
        {
            Expression<Func<PurchaseOrderPoco, bool>> query = (purchaseOrder)
                => (request.Number == Guid.Empty || request.Number == purchaseOrder.Number) &&
                   (request.StartDate == null || request.EndDate == null ? request.StartDate == purchaseOrder.Date : purchaseOrder.Date.IsBetween(request.StartDate, request.EndDate));

            return GetAllAsync<PurchaseOrderDto>(request, query);
        }

        public async Task<PurchaseOrderDto> GetPurchaseOrderAsync(IRequestDto<Guid> key)
        {
            var entity = await GetAsync(key);

            return entity.MapTo<PurchaseOrderDto>();
        }
    }
}
