using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Dto.Response;
using Tnf.Dto.Request;
using Tnf.Dto.Interfaces;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IWhiteHouseAppService : IApplicationService
    {
        Task<SuccessResponseListDto<PresidentDto>> GetAllPresidents(GetAllPresidentsDto request);
        Task<PresidentDto> GetPresidentById(RequestDto<string> id);
        Task<IResponseDto> InsertPresidentAsync(PresidentDto dto, bool sync = true);
        Task<IResponseDto> UpdatePresidentAsync(PresidentDto dto);
        Task<IResponseDto> DeletePresidentAsync(string id);
    }
}