using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IProfessionalDapperRepository : IRepository
    {
        ListDto<ProfessionalDto, ProfessionalKeysDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> requestDto);
        bool ExistsProfessional(ProfessionalKeysDto keys);
    }
}
