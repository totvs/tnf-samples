using Tnf.Application.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface ISpecialtyAppService : IApplicationService
    {
        SuccessResponseListDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request);
        SpecialtyDto GetSpecialty(RequestDto<int> requestDto);
        IResponseDto DeleteSpecialty(int id);
        IResponseDto CreateSpecialty(SpecialtyDto dto);
        IResponseDto UpdateSpecialty(SpecialtyDto dto);
    }
}
