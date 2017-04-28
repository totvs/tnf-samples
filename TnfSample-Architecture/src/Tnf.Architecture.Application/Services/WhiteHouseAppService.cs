using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Architecture.Dto;

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

        public async Task<PagingDtoResponse<PresidentDto>> GetAllPresidents(GellAllPresidentsRequestDto request)
            => await _whiteHouserService.GetAllPresidents(request);

        public async Task<PresidentDto> GetPresidentById(string id)
        {
            var president = await _whiteHouserService.GetPresidentById(id);

            if (president != null)
                president = new PresidentDto(president.Id, president.Name, president.ZipCode.Number);

            return president;
        }

        public async Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> dtos, bool sync = true)
            => await _whiteHouserService.InsertPresidentAsync(dtos, sync);

        public async Task<DtoResponseBase> UpdatePresidentAsync(PresidentDto dto)
            => await _whiteHouserService.UpdatePresidentAsync(dto);
    }
}
