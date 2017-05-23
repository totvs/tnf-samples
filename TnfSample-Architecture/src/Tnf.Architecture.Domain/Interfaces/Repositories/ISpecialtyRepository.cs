using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface ISpecialtyRepository : IRepository
    {
        SuccessResponseListDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request);
        SpecialtyDto GetSpecialty(RequestDto<int> requestDto);
        int CreateSpecialty(Specialty dto);
        Specialty UpdateSpecialty(Specialty dto);
        void DeleteSpecialty(int id);
        bool ExistsSpecialty(int id);
    }
}
