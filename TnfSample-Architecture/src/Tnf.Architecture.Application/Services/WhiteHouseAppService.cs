using System.Threading.Tasks;
using Tnf.App.Application.Services;
using Tnf.App.Bus.Notifications;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Carol.ReadInterfaces;
using Tnf.Architecture.Common;
using Tnf.Architecture.Common.Enumerables;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.AutoMapper;

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

        public Task<ListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request)
            => _readRepository.GetAllPresidents(request);

        public async Task<PresidentDto> GetPresidentById(RequestDto<string> id)
        {
            if (string.IsNullOrWhiteSpace(id.GetId()))
                RaiseNotification(nameof(id));

            if (Notification.HasNotification())
                return new PresidentDto();

            var entity = await _whiteHouserService.GetPresidentById(id);

            return entity.MapTo<PresidentDto>();
        }

        public async Task<PresidentDto> InsertPresidentAsync(PresidentDto dto)
        {
            if (dto == null)
                RaiseNotification(nameof(dto));

            if (Notification.HasNotification())
                return new PresidentDto();

            var builder = new PresidentBuilder()
                .WithId(dto.Id)
                .WithName(dto.Name)
                .WithAddress(dto.Address);

            dto.Id = await _whiteHouserService.InsertPresidentAsync(builder);

            return dto;
        }

        public async Task<PresidentDto> UpdatePresidentAsync(string id, PresidentDto dto)
        {
            if (string.IsNullOrEmpty(id))
                RaiseNotification(nameof(id));

            if (dto == null)
                RaiseNotification(nameof(dto));

            if (Notification.HasNotification())
                return new PresidentDto();

            var builder = new PresidentBuilder()
                .WithId(id)
                .WithName(dto.Name)
                .WithAddress(dto.Address);

            await _whiteHouserService.UpdatePresidentAsync(builder);

            dto.Id = id;
            return dto;
        }

        public async Task DeletePresidentAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                RaiseNotification(nameof(id));

            if (!Notification.HasNotification())
                await _whiteHouserService.DeletePresidentAsync(id);
        }

        private void RaiseNotification(params object[] parameter)
        {
            Notification.Raise(NotificationEvent.DefaultBuilder
                                                .WithMessage(AppConsts.LocalizationSourceName, Error.InvalidParameter)
                                                .WithDetailedMessage(AppConsts.LocalizationSourceName, Error.InvalidParameter)
                                                .WithMessageFormat(parameter)
                                                .Build());
        }
    }
}
