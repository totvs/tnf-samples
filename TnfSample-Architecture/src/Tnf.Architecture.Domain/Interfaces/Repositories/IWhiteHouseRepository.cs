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
        Task<PresidentDto> UpdatePresidentsAsync(PresidentDto president);
        Task<bool> DeletePresidentsAsync(PresidentDto president);
        Task<bool> DeletePresidentsAsync(string id);
    }
}
