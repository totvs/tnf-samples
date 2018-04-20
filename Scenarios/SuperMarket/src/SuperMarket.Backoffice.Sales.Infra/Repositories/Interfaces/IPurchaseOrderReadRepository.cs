using SuperMarket.Backoffice.Sales.Dto;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces
{
    public interface IPurchaseOrderReadRepository : IRepository
    {
        Task<PurchaseOrderDto> GetPurchaseOrderAsync(DefaultRequestDto key);

        Task<IListDto<PurchaseOrderDto>> GetAllPurchaseOrdersAsync(PurchaseOrderRequestAllDto request);
    }
}
