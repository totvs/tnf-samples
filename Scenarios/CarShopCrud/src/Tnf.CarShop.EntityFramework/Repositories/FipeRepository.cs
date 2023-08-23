using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;

using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Tnf.CarShop.EntityFrameworkCore.Repositories;

public class FipeRepository : EfCoreRepositoryBase<CarShopDbContext, Fipe>, IFipeRepository
{
    public FipeRepository(IDbContextProvider<CarShopDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public Task<IEnumerable<Fipe>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Fipe> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Fipe> GetByFipeCodeAsync(string fipeCode, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Fipe> UpdateAsync(Fipe fipe, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
