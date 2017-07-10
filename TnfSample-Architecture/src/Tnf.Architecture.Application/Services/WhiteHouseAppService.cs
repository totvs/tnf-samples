using System.Threading.Tasks;
using Tnf.App.Application.Services;
using Tnf.App.Bus.Notifications;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Enumerables;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Application.Services
{
    [RemoteService(false)]
    public class WhiteHouseAppService : AppApplicationService, IWhiteHouseAppService
    {
        private readonly IWhiteHouseService _whiteHouserService;

        public WhiteHouseAppService(IWhiteHouseService whiteHouserService)
        {
            _whiteHouserService = whiteHouserService;
        }

        public Task<ListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request)
            => _whiteHouserService.GetAllPresidents(request);

        public async Task<PresidentDto> GetPresidentById(RequestDto<string> id)
        {
            if (string.IsNullOrWhiteSpace(id.GetId()))
                RaiseNotification(nameof(id));

            if (Notification.HasNotification())
                return new PresidentDto();

            return await _whiteHouserService.GetPresidentById(id);
        }

        public async Task<PresidentDto> InsertPresidentAsync(PresidentDto president, bool sync = true)
        {
            if (president == null)
                RaiseNotification(nameof(president));

            if (Notification.HasNotification())
                return new PresidentDto();

            return await _whiteHouserService.InsertPresidentAsync(president, sync);
        }

        public async Task<PresidentDto> UpdatePresidentAsync(string id, PresidentDto president)
        {
            if (string.IsNullOrEmpty(id))
                RaiseNotification(nameof(id));

            if (president == null)
                RaiseNotification(nameof(president));

            if (Notification.HasNotification())
                return new PresidentDto();

            president.Id = id;
            return await _whiteHouserService.UpdatePresidentAsync(president);
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
