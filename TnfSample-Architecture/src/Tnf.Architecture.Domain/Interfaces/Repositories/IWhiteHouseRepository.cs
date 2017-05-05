using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Domain.Repositories;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Domain.Interfaces.Repositories
{
    public interface IWhiteHouseRepository : IRepository
    {
        Task<PagingResponseDto<PresidentDto>> GetAllPresidents(GellAllPresidentsDto request);
        Task<PresidentDto> GetPresidentById(string id);
        Task<List<PresidentDto>> InsertPresidentsAsync(List<PresidentDto> presidents, bool sync = false);
        Task UpdatePresidentsAsync(PresidentDto president);
        Task DeletePresidentsAsync(PresidentDto president);
        Task DeletePresidentsAsync(string id);
    }
}
