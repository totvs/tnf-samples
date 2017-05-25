using Tnf.Application.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface ISpecialtyAppService : IApplicationService
    {
        IResponseDto GetAllSpecialties(GetAllSpecialtiesDto request);
        IResponseDto GetSpecialty(RequestDto<int> id);
        IResponseDto DeleteSpecialty(int id);
        IResponseDto CreateSpecialty(SpecialtyDto specialty);
        IResponseDto UpdateSpecialty(int id, SpecialtyDto specialty);
    }
}
