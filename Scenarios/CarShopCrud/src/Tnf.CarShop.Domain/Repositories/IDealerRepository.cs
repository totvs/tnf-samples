using Tnf.CarShop.Domain.Entities;
using Tnf.Repositories;

namespace Tnf.CarShop.Domain.Repositories
{
    public interface IDealerRepository : IRepository
    {
        Task<Dealer> GetAsync(Guid dealerId, CancellationToken cancellationToken = default);
        Task<List<Dealer>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Dealer> InsertAsync(Dealer dealer, CancellationToken cancellationToken = default);
        Task<Dealer> UpdateAsync(Dealer dealer, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid dealerId, CancellationToken cancellationToken = default);
    }
}