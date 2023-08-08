using Microsoft.EntityFrameworkCore;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Tnf.CarShop.EntityFrameworkCore.Repositories;

public class PurchaseRepository : EfCoreRepositoryBase<CarShopDbContext, Purchase>, IPurchaseRepository
{
    public PurchaseRepository(IDbContextProvider<CarShopDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task DeleteAsync(Guid purchaseId, CancellationToken cancellationToken = default)
    {
        var purchase = await GetAsync(purchaseId, cancellationToken);
        if (purchase is null)
            return;

        await base.DeleteAsync(purchase, cancellationToken);
    }

    public async Task<List<Purchase>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await GetAllListAsync(cancellationToken);
    }

    public async Task<Purchase> GetAsync(Guid purchaseId, CancellationToken cancellationToken = default)
    {
        return await Table
            .Include(cr => cr.Car)
            .Include(ct => ct.Customer)
            .FirstOrDefaultAsync(x => x.Id == purchaseId, cancellationToken);
    }

    public async Task<Purchase> UpdateAsync(Purchase purchase, CancellationToken cancellationToken = default)
    {
        return await base.UpdateAsync(purchase, cancellationToken);
    }
}