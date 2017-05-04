using Tnf.Application.Services;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IProfessionalAppService : IApplicationService
    {
        PagingDtoResponse<ProfessionalDto> GetAllProfessionals();
    }
}
