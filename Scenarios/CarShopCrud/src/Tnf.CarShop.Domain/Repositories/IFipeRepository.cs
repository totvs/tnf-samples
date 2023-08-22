using Tnf.CarShop.Domain.Entities;
using Tnf.Repositories;

namespace Tnf.CarShop.Domain.Repositories;
public interface IFipeRepository : IRepository
{
    Task<Fipe> InsertAsync(Fipe fipe, CancellationToken cancellationToken = default);
    Task<Fipe> UpdateAsync(Fipe fipe, CancellationToken cancellationToken = default);
    Task<Fipe> GetAsync(Guid id, CancellationToken cancellationToken= default);
    Task<IEnumerable<Fipe>> GetAllAsync(CancellationToken cancellationToken= default);
}
