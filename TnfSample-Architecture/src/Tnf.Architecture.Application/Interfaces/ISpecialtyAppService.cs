using Tnf.Application.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface ISpecialtyAppService : IApplicationService
    {
        PagingResponseDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request);
        SpecialtyDto GetSpecialty(int id);
        ResponseDtoBase DeleteSpecialty(int id);
        ResponseDtoBase<SpecialtyDto> CreateSpecialty(SpecialtyDto dto);
        ResponseDtoBase<SpecialtyDto> UpdateSpecialty(SpecialtyDto dto);
    }
}
