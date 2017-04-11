using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Application.Services
{
    public interface IWhiteHouseAppService : IApplicationService
    {
        Task<PagingDtoResponse<PresidentDto>> GetAllPresidents(GellAllPresidentsRequestDto request);
        Task<PresidentDto> GetPresidentById(string id);
        Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> dtos, bool sync = true);
        Task<DtoResponseBase> UpdatePresidentAsync(PresidentDto dto);
        Task<DtoResponseBase> DeletePresidentAsync(string id);
    }
}