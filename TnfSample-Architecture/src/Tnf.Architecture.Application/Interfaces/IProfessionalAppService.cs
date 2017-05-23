using Tnf.Application.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IProfessionalAppService : IApplicationService
    {
        SuccessResponseListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto dto);
        ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys);
        IResponseDto CreateProfessional(ProfessionalDto dto);
        IResponseDto UpdateProfessional(ProfessionalDto dto);
        IResponseDto DeleteProfessional(ProfessionalKeysDto keys);
    }
}
