using Microsoft.EntityFrameworkCore;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using TnfZero.Domain.Dtos;
using TnfZero.Domain.Entities;
using TnfZero.Domain.Repositories;

namespace TnfZero.EntityFramework.Repositories;

public class DogRepository(IDbContextProvider<TnfZeroDbContext> dbContextProvider)
    : EfCoreRepositoryBase<TnfZeroDbContext, DogEntity>(dbContextProvider),
        IDogRepository
{
    public async Task<DogEntity?> FindByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await GetAll().FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<IListDto<DogDto>> GetAllAsync(DogRequestAllDto request, CancellationToken ct = default)
    {
        return await GetAll()
            .Select(e => new DogDto(e.Id, e.Name))
            .ToListDtoAsync(request, ct);
    }
}