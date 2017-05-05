using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IWhiteHouseAppService : IApplicationService
    {
        Task<PagingResponseDto<PresidentDto>> GetAllPresidents(GellAllPresidentsDto request);
        Task<PresidentDto> GetPresidentById(string id);
        Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> dtos, bool sync = true);
        Task<DtoResponseBase> UpdatePresidentAsync(PresidentDto dto);
        Task<DtoResponseBase> DeletePresidentAsync(string id);
    }
}