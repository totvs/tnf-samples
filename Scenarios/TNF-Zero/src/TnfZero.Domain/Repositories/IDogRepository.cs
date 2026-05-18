using Tnf.Dto;
using Tnf.Repositories;
using TnfZero.Domain.Dtos;
using TnfZero.Domain.Entities;

namespace TnfZero.Domain.Repositories;

public interface IDogRepository : IRepository<DogEntity>
{
    Task<DogEntity?> FindByIdAsync(Guid id, CancellationToken ct = default);
    Task<IListDto<DogDto>> GetAllAsync(DogRequestAllDto request, CancellationToken ct = default);
}