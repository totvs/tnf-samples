using Tnf.Application.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.App.Dto.Response;
using Tnf.App.Dto.Request;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IProfessionalAppService : IApplicationService
    {
        ListDto<ProfessionalDto, ProfessionalKeysDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys);
        ProfessionalDto CreateProfessional(ProfessionalDto professional);
        ProfessionalDto UpdateProfessional(ProfessionalKeysDto keys, ProfessionalDto professional);
        void DeleteProfessional(ProfessionalKeysDto keys);
    }
}
