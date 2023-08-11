using Tnf.CarShop.Domain.Entities;
using Tnf.Repositories;

namespace Tnf.CarShop.Domain.Repositories;

public interface IStoreRepository : IRepository
{
    Task<Store> GetAsync(Guid tenantId, CancellationToken cancellationToken = default);
    Task<List<Store>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Store> InsertAsync(Store store, CancellationToken cancellationToken = default);
    Task<Store> UpdateAsync(Store store, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, CancellationToken cancellationToken = default);
}