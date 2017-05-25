using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface ISpecialtyService : IDomainService
    {
        SuccessResponseListDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request);
        IResponseDto GetSpecialty(RequestDto<int> requestDto);
        IResponseDto DeleteSpecialty(int id);
        IResponseDto CreateSpecialty(SpecialtyDto dto);
        IResponseDto UpdateSpecialty(SpecialtyDto dto);
    }
}
