using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface ISpecialtyRepository : IRepository
    {
        ListDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request);
        SpecialtyDto GetSpecialty(RequestDto<int> requestDto);
        int CreateSpecialty(Specialty dto);
        Specialty UpdateSpecialty(Specialty dto);
        void DeleteSpecialty(int id);
        bool ExistsSpecialty(int id);
    }
}
