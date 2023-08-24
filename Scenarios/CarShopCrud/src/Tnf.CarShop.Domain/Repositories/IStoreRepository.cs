using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Entities;
using Tnf.Dto;
using Tnf.Repositories;

namespace Tnf.CarShop.Domain.Repositories;

public interface IStoreRepository : IRepository
{
    Task<Store> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IListDto<StoreDto>> GetAllAsync(RequestAllDto requestAllDto, CancellationToken cancellationToken = default);
    Task<Store> InsertAsync(Store store, CancellationToken cancellationToken = default);
    Task<Store> UpdateAsync(Store store, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
