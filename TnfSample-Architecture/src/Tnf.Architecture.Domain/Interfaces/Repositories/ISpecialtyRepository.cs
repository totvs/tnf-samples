using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface ISpecialtyRepository : IRepository
    {
        SuccessResponseListDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request);
        SpecialtyDto GetSpecialty(RequestDto requestDto);
        void DeleteSpecialty(int id);
        SpecialtyDto CreateSpecialty(SpecialtyDto dto);
        SpecialtyDto UpdateSpecialty(SpecialtyDto dto);
        bool ExistsSpecialty(int id);
    }
}
