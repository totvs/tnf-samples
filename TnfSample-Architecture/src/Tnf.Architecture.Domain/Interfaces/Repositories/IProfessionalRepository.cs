using System.Collections.Generic;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IProfessionalRepository : IRepository
    {
        PagingResponseDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(ProfessionalKeysDto keys);
        ProfessionalDto CreateProfessional(ProfessionalCreateDto entity);
        ProfessionalDto UpdateProfessional(ProfessionalDto dto);
        void DeleteProfessional(ProfessionalKeysDto keys);
        void AddOrRemoveSpecialties(ProfessionalKeysDto keys, List<SpecialtyDto> dto);
    }
}
