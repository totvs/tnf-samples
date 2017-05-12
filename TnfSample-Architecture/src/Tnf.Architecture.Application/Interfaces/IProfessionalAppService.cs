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
        DtoResponseBase<ProfessionalDto> CreateProfessional(ProfessionalDto dto);
        DtoResponseBase<ProfessionalDto> UpdateProfessional(ProfessionalDto dto);
        DtoResponseBase DeleteProfessional(ProfessionalKeysDto keys);
    }
}
