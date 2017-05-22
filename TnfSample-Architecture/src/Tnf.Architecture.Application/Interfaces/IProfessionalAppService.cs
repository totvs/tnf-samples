using Tnf.Application.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IProfessionalAppService : IApplicationService
    {
        PagingResponseDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto dto);
        ProfessionalDto GetProfessional(ProfessionalKeysDto keys);
        ResponseDtoBase<ProfessionalDto> CreateProfessional(ProfessionalDto dto);
        ResponseDtoBase<ProfessionalDto> UpdateProfessional(ProfessionalDto dto);
        ResponseDtoBase DeleteProfessional(ProfessionalKeysDto keys);
    }
}
