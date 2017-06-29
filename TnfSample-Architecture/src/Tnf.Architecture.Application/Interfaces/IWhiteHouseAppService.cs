using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IWhiteHouseAppService : IApplicationService
    {
        Task<ListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request);
        Task<PresidentDto> GetPresidentById(RequestDto<string> id);
        Task<PresidentDto> InsertPresidentAsync(PresidentDto president, bool sync = true);
        Task<PresidentDto> UpdatePresidentAsync(string id, PresidentDto president);
        Task DeletePresidentAsync(string id);
    }
}