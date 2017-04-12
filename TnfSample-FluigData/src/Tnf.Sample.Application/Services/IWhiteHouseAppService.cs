using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Sample.Dto;

namespace Tnf.Sample.Application.Services
{
    public interface IWhiteHouseAppService : IApplicationService
    {
        Task<PagingDtoResponse<PresidentDto>> GetAllPresidents(GellAllPresidentsRequestDto request);
        Task<PresidentDto> GetPresidentById(string id);
        Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> dtos);
        Task<DtoResponseBase> UpdatePresidentAsync(PresidentDto dto);
        Task<DtoResponseBase> DeletePresidentAsync(string id);
    }
}