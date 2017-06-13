using System.Threading.Tasks;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.App.Dto.Response;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Enumerables;
using Tnf.App.Bus.Notifications;

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

        public async Task<ListDto<PresidentDto>> GetAllPresidents(GetAllPresidentsDto request)
        {
            if (request.PageSize <= 0)
                RaiseNotification(nameof(request.PageSize));

            if (Notification.HasNotification())
                return new ListDto<PresidentDto>();

            return await _whiteHouserService.GetAllPresidents(request);
        }

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

        private static void RaiseNotification(params string[] parameter)
        {
            Notification.Raise(NotificationEvent.DefaultBuilder
                                                .WithMessage(AppConsts.LocalizationSourceName, Error.InvalidParameter)
                                                .WithDetailedMessage(AppConsts.LocalizationSourceName, Error.InvalidParameterDynamic)
                                                .WithMessageFormat(parameter)
                                                .Build());
        }
    }
}
