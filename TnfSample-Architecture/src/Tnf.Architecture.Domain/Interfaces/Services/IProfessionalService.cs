using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IProfessionalService : IDomainService
    {
        SuccessResponseListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys);
        IResponseDto CreateProfessional(ProfessionalDto dto);
        IResponseDto UpdateProfessional(ProfessionalDto dto);
        IResponseDto DeleteProfessional(ProfessionalKeysDto keys);
    }
}
