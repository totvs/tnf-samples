using Microsoft.EntityFrameworkCore;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;

using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Tnf.CarShop.EntityFrameworkCore.Repositories
{
    public class DealerRepository : EfCoreRepositoryBase<CarShopDbContext, Dealer>, IDealerRepository
    {
        public DealerRepository(IDbContextProvider<CarShopDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task DeleteAsync(Guid dealerId, CancellationToken cancellationToken = default)
        {
            var dealer = await GetAsync(dealerId, cancellationToken);
            if (dealer is null)
                return;

            await base.DeleteAsync(dealer, cancellationToken);
        }

        public async Task<List<Dealer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetAllListAsync(cancellationToken);
        }

        public async Task<Dealer> GetAsync(Guid dealerId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Include(c => c.Cars)
                .FirstOrDefaultAsync(x => x.Id == dealerId);
        }

        public async Task<Dealer> UpdateAsync(Dealer dealer, CancellationToken cancellationToken = default)
        {
            return await base.UpdateAsync(dealer, cancellationToken);
        }
    }
}
