using Microsoft.EntityFrameworkCore;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Dto;
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

    public async Task<IListDto<CarDto>> GetAllAsync(RequestAllDto requestAllDto, CancellationToken cancellationToken = default)
    {
        var basequery = GetAll().AsNoTracking();

        return await basequery.Select(x => new CarDto
        {
            Id = x.Id,
            Brand = x.Brand,
            Model = x.Model,
            Price = x.Price,
            StoreId = x.StoreId,
            Year = x.Year,
        }).ToListDtoAsync(requestAllDto, cancellationToken);
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
