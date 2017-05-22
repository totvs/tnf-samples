using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IProfessionalService : IDomainService
    {
        PagingResponseDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request);
        ProfessionalDto GetProfessional(ProfessionalKeysDto keys);
        ResponseDtoBase<ProfessionalDto> CreateProfessional(ProfessionalDto entity);
        ResponseDtoBase<ProfessionalDto> UpdateProfessional(ProfessionalDto dto);
        ResponseDtoBase DeleteProfessional(ProfessionalKeysDto keys);
    }
}
