using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Domain.Services;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Domain.Interfaces.Services
{
    public interface IWhiteHouseService : IDomainService
    {
        Task<SuccessResponseListDto<PresidentDto>> GetAllPresidents(GetAllPresidentsDto request);
        Task<IResponseDto> GetPresidentById(RequestDto<string> id);
        Task<IResponseDto> InsertPresidentAsync(PresidentDto request, bool sync = false);
        Task<IResponseDto> UpdatePresidentAsync(PresidentDto president);
        Task<IResponseDto> DeletePresidentAsync(string id);
    }
}