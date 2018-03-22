using SuperMarket.Backoffice.Sales.Dto;
using System;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces
{
    public interface IPurchaseOrderReadRepository : IRepository
    {
        Task<PurchaseOrderDto> GetPurchaseOrderAsync(IRequestDto<Guid> key);

        Task<IListDto<PurchaseOrderDto, Guid>> GetAllPurchaseOrdersAsync(PurchaseOrderRequestAllDto request);
    }
}
