using Microsoft.EntityFrameworkCore;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Tnf.CarShop.EntityFrameworkCore.Repositories;

public class CarRepository : EfCoreRepositoryBase<CarShopDbContext, Car>, ICarRepository
{
    public CarRepository(IDbContextProvider<CarShopDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task DeleteAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        var car = await GetAsync(carId, cancellationToken);
        if (car is null)
            return;

        await base.DeleteAsync(car, cancellationToken);
    }

    public Task<List<Car>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return base.GetAllListAsync(cancellationToken);
    }

    public async Task<Car> GetAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        return await Table.FirstOrDefaultAsync(x => x.Id == carId, cancellationToken);
    }

    public async Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken = default)
    {
        return await base.UpdateAsync(car, cancellationToken);
    }
}