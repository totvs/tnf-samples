using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Sample.Core.Interfaces.Services;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Sample.Dto;

namespace Tnf.Sample.Application.Services
{
    public class WhiteHouseAppService : ApplicationService, IWhiteHouseAppService
    {
        private readonly IWhiteHouseService _whiteHouserService;

        public WhiteHouseAppService(IWhiteHouseService whiteHouserService)
        {
            _whiteHouserService = whiteHouserService;
        }

        public async Task<DtoResponseBase> DeletePresidentAsync(string id)
        {
            return await _whiteHouserService.DeletePresidentAsync(id);
        }

        public async Task<PagingDtoResponse<PresidentDto>> GetAllPresidents(GellAllPresidentsRequestDto request)
        {
            var response = await _whiteHouserService.GetAllPresidents(request);
            return response;
        }

        public async Task<PresidentDto> GetPresidentById(string id)
        {
            var president = await _whiteHouserService.GetPresidentById(id);

            return new PresidentDto()
            {
                Id = president.Id,
                Name = president.Name,
                ZipCode = president.ZipCode
            };
        }

        public async Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> dtos)
        {
            var response = await _whiteHouserService.InsertPresidentAsync(dtos, true);
            return response;
        }

        public async Task<DtoResponseBase> UpdatePresidentAsync(PresidentDto dto)
        {
            return await _whiteHouserService.UpdatePresidentAsync(dto);
        }
    }
}
