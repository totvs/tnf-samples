using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IProfessionalService : IDomainService
    {
        ListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys);
        ProfessionalDto CreateProfessional(ProfessionalDto dto);
        ProfessionalDto UpdateProfessional(ProfessionalDto dto);
        void DeleteProfessional(ProfessionalKeysDto keys);
    }
}
