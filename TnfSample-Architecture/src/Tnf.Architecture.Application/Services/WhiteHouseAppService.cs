using System.Threading.Tasks;
using Tnf.App.Application.Enums;
using Tnf.App.Application.Services;
using Tnf.App.AutoMapper;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Carol.ReadInterfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Application.Services
{
    [RemoteService(false)]
    public class WhiteHouseAppService : AppApplicationService, IWhiteHouseAppService
    {
        private readonly IWhiteHouseService _whiteHouserService;
        private readonly IWhiteHouseReadRepository _readRepository;

        public WhiteHouseAppService(IWhiteHouseService whiteHouserService, IWhiteHouseReadRepository readRepository)
        {
            _whiteHouserService = whiteHouserService;
            _readRepository = readRepository;
        }

        public Task<IListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request)
            => _readRepository.GetAllPresidents(request);

        public async Task<PresidentDto> GetPresidentById(IRequestDto<string> id)
        {
            ValidateRequestDto<IRequestDto<string>, string>(id);

            if (string.IsNullOrWhiteSpace(id.GetId()))
                RaiseNotification(TnfAppApplicationErrors.AppApplicationOnInvalidIdError);

            if (Notification.HasNotification())
                return PresidentDto.NullInstance;

            var entity = await _whiteHouserService.GetPresidentById(id);

            return entity.MapTo<PresidentDto>();
        }

        public async Task<PresidentDto> InsertPresidentAsync(PresidentDto dto)
        {
            ValidateDto<PresidentDto, string>(dto);

            if (Notification.HasNotification())
                return PresidentDto.NullInstance;

            var builder = new PresidentBuilder(Notification)
                .WithId(dto.Id)
                .WithName(dto.Name)
                .WithAddress(dto.Address);

            dto.Id = await _whiteHouserService.InsertPresidentAsync(builder);
            return dto;
        }

        public async Task<PresidentDto> UpdatePresidentAsync(string id, PresidentDto dto)
        {
            ValidateDtoAndId(dto, id);

            if (string.IsNullOrWhiteSpace(id))
                RaiseNotification(TnfAppApplicationErrors.AppApplicationOnInvalidIdError);

            if (Notification.HasNotification())
                return PresidentDto.NullInstance;

            var builder = new PresidentBuilder(Notification)
                .WithId(id)
                .WithName(dto.Name)
                .WithAddress(dto.Address);

            await _whiteHouserService.UpdatePresidentAsync(builder);

            dto.Id = id;
            return dto;
        }

        public async Task DeletePresidentAsync(string id)
        {
            ValidateId(id);

            if (string.IsNullOrWhiteSpace(id))
                RaiseNotification(TnfAppApplicationErrors.AppApplicationOnInvalidIdError);

            if (Notification.HasNotification())
                return;

            await _whiteHouserService.DeletePresidentAsync(id);
        }
    }
}
