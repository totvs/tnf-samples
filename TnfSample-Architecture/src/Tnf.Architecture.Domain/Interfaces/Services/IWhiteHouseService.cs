using System.Threading.Tasks;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IWhiteHouseService : IDomainService
    {
        Task<ListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request);
        Task<PresidentDto> GetPresidentById(RequestDto<string> id);
        Task<PresidentDto> InsertPresidentAsync(PresidentDto request, bool sync = false);
        Task<PresidentDto> UpdatePresidentAsync(PresidentDto president);
        Task DeletePresidentAsync(string id);
    }
}