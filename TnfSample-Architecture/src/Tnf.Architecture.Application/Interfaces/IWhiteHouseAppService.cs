using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Dto.Request;
using Tnf.Dto.Interfaces;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IWhiteHouseAppService : IApplicationService
    {
        Task<IResponseDto> GetAllPresidents(GetAllPresidentsDto request);
        Task<IResponseDto> GetPresidentById(RequestDto<string> id);
        Task<IResponseDto> InsertPresidentAsync(PresidentDto president, bool sync = true);
        Task<IResponseDto> UpdatePresidentAsync(string id, PresidentDto president);
        Task<IResponseDto> DeletePresidentAsync(string id);
    }
}