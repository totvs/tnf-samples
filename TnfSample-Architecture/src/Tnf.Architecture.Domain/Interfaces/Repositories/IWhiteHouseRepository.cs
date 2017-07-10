using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IWhiteHouseRepository : IRepository
    {
        Task<ListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request);
        Task<PresidentDto> GetPresidentById(RequestDto<string> requestDto);
        Task<List<string>> InsertPresidentsAsync(List<President> presidents, bool sync = false);
        Task<President> UpdatePresidentsAsync(President president);
        Task<bool> DeletePresidentsAsync(string id);
    }
}
