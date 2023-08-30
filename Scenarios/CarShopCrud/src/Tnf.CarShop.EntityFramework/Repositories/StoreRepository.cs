using Microsoft.EntityFrameworkCore;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Tnf.CarShop.EntityFrameworkCore.Repositories;

public class StoreRepository : EfCoreRepositoryBase<CarShopDbContext, Store>, IStoreRepository
{
    public StoreRepository(IDbContextProvider<CarShopDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var store = await GetAsync(id, cancellationToken);
        if (store is null)
            return;

        await base.DeleteAsync(store, cancellationToken);
    }

    public async Task<IListDto<StoreDto>> GetAllAsync(RequestAllDto requestAllDto, CancellationToken cancellationToken = default)
    {
        var baseQuery = GetAll().AsNoTracking();

        return await baseQuery.Select(x => new StoreDto
        {
            Id = x.Id,
            Name = x.Name,
            Location = x.Location,
            Cnpj = x.Cnpj
        }).ToListDtoAsync(requestAllDto, cancellationToken);
    }

    public async Task<Store> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Table
            .Include(c => c.Cars)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Store> UpdateAsync(Store store, CancellationToken cancellationToken = default)
    {
        return await base.UpdateAsync(store, cancellationToken);
    }
}
