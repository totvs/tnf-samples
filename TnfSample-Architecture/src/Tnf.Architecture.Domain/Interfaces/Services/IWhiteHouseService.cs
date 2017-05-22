using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IWhiteHouseService : IDomainService
    {
        Task<PagingResponseDto<PresidentDto>> GetAllPresidents(GetAllPresidentsDto request);
        Task<PresidentDto> GetPresidentById(string id);
        Task<ResponseDtoBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> request, bool sync = false);
        Task<ResponseDtoBase<PresidentDto>> UpdatePresidentAsync(PresidentDto president);
        Task<ResponseDtoBase> DeletePresidentAsync(string id);
    }
}