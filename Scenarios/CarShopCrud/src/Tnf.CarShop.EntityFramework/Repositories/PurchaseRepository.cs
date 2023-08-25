using Microsoft.EntityFrameworkCore;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Dto;
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

    public async Task<IListDto<PurchaseDto>> GetAllAsync(RequestAllDto requestAllDto, CancellationToken cancellationToken = default)
    {
        var baseQuery = GetAll()
            .Include(c => c.Car)
            .Include(s => s.Store)
            .Include(c => c.Customer)
            .AsNoTracking();

        return await baseQuery.Select(x => new PurchaseDto
        {
            Customer = x.Customer.ToDto(),
            Car = x.Car.ToDto(),
            Store = x.Store.ToDto(),
            Id = x.Id,
            PurchaseDate = x.PurchaseDate            
            
        }).ToListDtoAsync(requestAllDto, cancellationToken);
    }

    public async Task<Purchase> GetAsync(Guid purchaseId, CancellationToken cancellationToken = default)
    {
        return await Table
            .Include(cr => cr.Car)
            .Include(ct => ct.Customer)
            .Include(st => st.Store)
            .FirstOrDefaultAsync(x => x.Id == purchaseId, cancellationToken);
    }

    public async Task<PurchaseDto> GetPurchaseDtoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var purchase = await GetAsync(id, cancellationToken);

        if (purchase is null)
            return null;

        return new PurchaseDto
        {
            Customer = purchase.Customer.ToDto(),
            Car = purchase.Car.ToDto(),
            Store = purchase.Store.ToDto(),
            Id = purchase.Id,
            PurchaseDate = purchase.PurchaseDate
        };
    }

    public async Task<Purchase> UpdateAsync(Purchase purchase, CancellationToken cancellationToken = default)
    {
        return await base.UpdateAsync(purchase, cancellationToken);
    }
}
