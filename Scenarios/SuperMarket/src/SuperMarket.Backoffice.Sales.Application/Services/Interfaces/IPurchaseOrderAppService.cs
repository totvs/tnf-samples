using SuperMarket.Backoffice.Sales.Dto;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Sales.Application.Services.Interfaces
{
    public interface IPurchaseOrderAppService : IApplicationService
    {
        Task<PurchaseOrderDto> CreatePurchaseOrderAsync(PurchaseOrderDto product);
        Task<PurchaseOrderDto> UpdatePurchaseOrderAsync(Guid id, PurchaseOrderDto product);
        Task<PurchaseOrderDto> GetPurchaseOrderAsync(DefaultRequestDto id);
        Task<IListDto<PurchaseOrderDto>> GetAllPurchaseOrderAsync(PurchaseOrderRequestAllDto request);
    }
}
