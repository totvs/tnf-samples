using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Sample.Dto;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace Tnf.Sample.Core.Interfaces.Services
{
    public interface IWhiteHouseService : IDomainService
    {
        Task<PagingDtoResponse<PresidentDto>> GetAllPresidents(GellAllPresidentsRequestDto request);
        Task<PresidentDto> GetPresidentById(string id);
        Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> request, bool sync = false);
        Task<DtoResponseBase> UpdatePresidentAsync(PresidentDto president);
        Task<DtoResponseBase> DeletePresidentAsync(string id);
    }
}