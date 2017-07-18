using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Application.Services;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface ISpecialtyAppService : IApplicationService
    {
        ListDto<SpecialtyDto, int> GetAllSpecialties(GetAllSpecialtiesDto request);
        SpecialtyDto GetSpecialty(RequestDto id);
        void DeleteSpecialty(int id);
        SpecialtyDto CreateSpecialty(SpecialtyDto specialty);
        SpecialtyDto UpdateSpecialty(int id, SpecialtyDto specialty);
    }
}
