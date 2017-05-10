using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface ISpecialtyRepository : IRepository
    {
        PagingResponseDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request);
        SpecialtyDto GetSpecialty(int id);
        bool DeleteSpecialty(int id);
        SpecialtyDto CreateSpecialty(SpecialtyDto dto);
        SpecialtyDto UpdateSpecialty(SpecialtyDto dto);
    }
}
