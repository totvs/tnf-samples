using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IProfessionalService : IDomainService
    {
        ListDto<ProfessionalDto, ProfessionalKeysDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys);
        ProfessionalDto CreateProfessional(ProfessionalDto dto);
        ProfessionalDto UpdateProfessional(ProfessionalDto dto);
        void DeleteProfessional(ProfessionalKeysDto keys);
    }
}
