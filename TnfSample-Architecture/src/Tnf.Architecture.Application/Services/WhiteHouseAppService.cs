using System.Threading.Tasks;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.AutoMapper;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Response;
using Tnf.Dto.Request;

namespace Tnf.Architecture.Application.Services
{
    [RemoteService(false)]
    public class WhiteHouseAppService : ApplicationService, IWhiteHouseAppService
    {
        private readonly IWhiteHouseService _whiteHouserService;

        public WhiteHouseAppService(IWhiteHouseService whiteHouserService)
        {
            _whiteHouserService = whiteHouserService;
        }

        public async Task<IResponseDto> DeletePresidentAsync(string id)
            => await _whiteHouserService.DeletePresidentAsync(id);

        public async Task<SuccessResponseListDto<PresidentDto>> GetAllPresidents(GetAllPresidentsDto request)
            => await _whiteHouserService.GetAllPresidents(request);

        public async Task<PresidentDto> GetPresidentById(RequestDto<string> id)
        {
            var president = await _whiteHouserService.GetPresidentById(id);

            if (president != null)
                president = president.MapTo<PresidentDto>();

            return president;
        }

        public async Task<IResponseDto> InsertPresidentAsync(PresidentDto dto, bool sync = true)
            => await _whiteHouserService.InsertPresidentAsync(dto, sync);

        public async Task<IResponseDto> UpdatePresidentAsync(PresidentDto dto)
            => await _whiteHouserService.UpdatePresidentAsync(dto);
    }
}
