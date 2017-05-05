using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Paging;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IProfessionalService : IDomainService
    {
        PagingResponseDto<ProfessionalDto> All(GetAllProfessionalsDto request);
        ProfessionalDto Get(ProfessionalKeysDto keys);
        DtoResponseBase<ProfessionalDto> Create(ProfessionalCreateDto entity);
        DtoResponseBase<ProfessionalDto> Update(ProfessionalDto dto);
        void Delete(ProfessionalKeysDto keys);
    }
}
