using Tnf.CarShop.Domain.Entities;
using Tnf.Repositories;

namespace Tnf.CarShop.Domain.Repositories
{
    public interface IPurchaseRepository : IRepository
    {
        Task<Purchase> GetAsync(Guid purchaseId, CancellationToken cancellationToken = default);
        Task<List<Purchase>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Purchase> InsertAsync(Purchase purchase, CancellationToken cancellationToken = default);
        Task<Purchase> UpdateAsync(Purchase purchase, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid purchaseId, CancellationToken cancellationToken = default);
    }
}