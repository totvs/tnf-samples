using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Application.Services;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IWhiteHouseAppService : IApplicationService
    {
        Task<ListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request);
        Task<PresidentDto> GetPresidentById(RequestDto<string> id);
        Task<PresidentDto> InsertPresidentAsync(PresidentDto president);
        Task<PresidentDto> UpdatePresidentAsync(string id, PresidentDto president);
        Task DeletePresidentAsync(string id);
    }
}