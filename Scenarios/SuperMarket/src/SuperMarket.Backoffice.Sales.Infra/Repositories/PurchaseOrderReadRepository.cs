using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Infra.Contexts;
using SuperMarket.Backoffice.Sales.Infra.Pocos;
using SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces;
using System;
using System.Linq;
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
            var query = GetAll();

            if (request.Number.HasValue)
            {
                query = query.Where(p => p.Number == request.Number);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(p => p.Date >= request.StartDate);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(p => p.Date <= request.EndDate);
            }

            return query.ToListDtoAsync<PurchaseOrderPoco, PurchaseOrderDto>(request);
        }

        public async Task<PurchaseOrderDto> GetPurchaseOrderAsync(DefaultRequestDto key)
        {
            var entity = await GetAsync(key);

            return entity.MapTo<PurchaseOrderDto>();
        }
    }
}
