using Tnf.Application.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Paging;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IProfessionalAppService : IApplicationService
    {
        PagingResponseDto<ProfessionalDto> All(GetAllProfessionalsDto dto);
        ProfessionalDto Get(ProfessionalKeysDto keys);
        DtoResponseBase<ProfessionalDto> Create(ProfessionalCreateDto dto);
        DtoResponseBase<ProfessionalDto> Update(ProfessionalDto dto);
        void Delete(ProfessionalKeysDto keys);
    }
}
