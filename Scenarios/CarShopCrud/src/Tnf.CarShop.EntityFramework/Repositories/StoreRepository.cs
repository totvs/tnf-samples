using Microsoft.EntityFrameworkCore;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Tnf.CarShop.EntityFrameworkCore.Repositories;

public class StoreRepository : EfCoreRepositoryBase<CarShopDbContext, Store>, IStoreRepository
{
    public StoreRepository(IDbContextProvider<CarShopDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task DeleteAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        var store = await GetAsync(tenantId, cancellationToken);
        if (store is null)
            return;

        await base.DeleteAsync(store, cancellationToken);
    }

    public async Task<List<Store>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await GetAllListAsync(cancellationToken);
    }

    public async Task<Store> GetAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await Table
            //.Include(c => c.Cars)
            .FirstOrDefaultAsync(x => x.TenantId == tenantId);
    }

    public async Task<Store> UpdateAsync(Store store, CancellationToken cancellationToken = default)
    {
        return await base.UpdateAsync(store, cancellationToken);
    }
}
