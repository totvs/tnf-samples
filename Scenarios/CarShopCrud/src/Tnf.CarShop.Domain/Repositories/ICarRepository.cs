using Tnf.CarShop.Domain.Entities;
using Tnf.Repositories;

namespace Tnf.CarShop.Domain.Repositories;

public interface ICarRepository : IRepository
{
    Task<Car> GetAsync(Guid carId, CancellationToken cancellationToken = default);
    Task<List<Car>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Car> InsertAsync(Car car, CancellationToken cancellationToken = default);
    Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid carId, CancellationToken cancellationToken = default);
}