using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Entities;

using Tnf.Dto;

using Tnf.Repositories;

namespace Tnf.CarShop.Domain.Repositories;

public interface IPurchaseRepository : IRepository
{
    Task<Purchase> GetAsync(Guid purchaseId, CancellationToken cancellationToken = default);
    Task<IListDto<PurchaseDto>> GetAllAsync(RequestAllDto requestAllDto, CancellationToken cancellationToken = default);
    Task<PurchaseDto> GetPurchaseDtoAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Purchase> InsertAsync(Purchase purchase, CancellationToken cancellationToken = default);
    Task<Purchase> UpdateAsync(Purchase purchase, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid purchaseId, CancellationToken cancellationToken = default);
}
