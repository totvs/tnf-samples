using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.AutoMapper;

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

        public async Task<DtoResponseBase> DeletePresidentAsync(string id)
            => await _whiteHouserService.DeletePresidentAsync(id);

        public async Task<PagingResponseDto<PresidentDto>> GetAllPresidents(GellAllPresidentsDto request)
            => await _whiteHouserService.GetAllPresidents(request);

        public async Task<PresidentDto> GetPresidentById(string id)
        {
            var president = await _whiteHouserService.GetPresidentById(id);

            if (president != null)
                president = president.MapTo<PresidentDto>();

            return president;
        }

        public async Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> dtos, bool sync = true)
            => await _whiteHouserService.InsertPresidentAsync(dtos, sync);

        public async Task<DtoResponseBase> UpdatePresidentAsync(PresidentDto dto)
            => await _whiteHouserService.UpdatePresidentAsync(dto);
    }
}
