using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Entities;
using Tnf.Dto;
using Tnf.Repositories;

namespace Tnf.CarShop.Domain.Repositories;

public interface ICarRepository : IRepository
{
    Task<Car> GetAsync(Guid carId, CancellationToken cancellationToken = default);
    Task<IListDto<CarDto>> GetAllAsync(RequestAllDto requestAllDto, CancellationToken cancellationToken = default);
    Task<Car> InsertAsync(Car car, CancellationToken cancellationToken = default);
    Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid carId, CancellationToken cancellationToken = default);
}
