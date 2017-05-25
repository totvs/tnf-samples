using Tnf.Application.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IProfessionalAppService : IApplicationService
    {
        IResponseDto GetAllProfessionals(GetAllProfessionalsDto request);
        IResponseDto GetProfessional(RequestDto<ProfessionalKeysDto> keys);
        IResponseDto CreateProfessional(ProfessionalDto professional);
        IResponseDto UpdateProfessional(ProfessionalKeysDto keys, ProfessionalDto professional);
        IResponseDto DeleteProfessional(ProfessionalKeysDto keys);
    }
}
