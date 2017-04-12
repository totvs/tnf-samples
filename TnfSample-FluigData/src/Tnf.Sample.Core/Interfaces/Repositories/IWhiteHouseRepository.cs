using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Domain.Repositories;
using Tnf.Sample.Dto;

namespace Tnf.Sample.Core.Interfaces.Repositories
{
    public interface IWhiteHouseRepository : IRepository
    {
        Task<PagingDtoResponse<PresidentDto>> GetAllPresidents(GellAllPresidentsRequestDto request);
        Task<PresidentDto> GetPresidentById(string id);
        Task<List<PresidentDto>> InsertPresidentsAsync(List<PresidentDto> presidents, bool sync = false);
        Task UpdatePresidentsAsync(PresidentDto president);
        Task DeletePresidentsAsync(PresidentDto president);
        Task DeletePresidentsAsync(string id);
    }
}
