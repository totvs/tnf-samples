using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Tnf.CarShop.EntityFrameworkCore.Repositories
{
    public class CarRepository : EfCoreRepositoryBase<CarShopDbContext, Car>, ICarRepository
    {
        public CarRepository(IDbContextProvider<CarShopDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<bool> DeleteAsync(Guid carId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Car>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Car> GetAsync(Guid carId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
