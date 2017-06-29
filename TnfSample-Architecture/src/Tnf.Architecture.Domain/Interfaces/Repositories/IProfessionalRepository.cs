using System.Collections.Generic;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IProfessionalRepository : IRepository
    {
        ListDto<ProfessionalDto, ProfessionalKeysDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> requestDto);
        ProfessionalKeysDto CreateProfessional(Professional entity);
        Professional UpdateProfessional(Professional dto);
        bool DeleteProfessional(ProfessionalKeysDto keys);
        void AddOrRemoveSpecialties(ProfessionalKeysDto keys, List<SpecialtyDto> dto);
        bool ExistsProfessional(ProfessionalKeysDto keys);
    }
}
