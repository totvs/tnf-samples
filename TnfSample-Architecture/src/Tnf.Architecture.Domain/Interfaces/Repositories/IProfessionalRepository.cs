using System.Collections.Generic;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IProfessionalRepository : IRepository
    {
        SuccessResponseListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> requestDto);
        ProfessionalKeysDto CreateProfessional(Professional entity);
        Professional UpdateProfessional(Professional dto);
        bool DeleteProfessional(ProfessionalKeysDto keys);
        void AddOrRemoveSpecialties(ProfessionalKeysDto keys, List<SpecialtyDto> dto);
        bool ExistsProfessional(ProfessionalKeysDto keys);
    }
}
