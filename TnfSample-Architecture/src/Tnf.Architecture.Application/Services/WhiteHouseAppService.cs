using System.Threading.Tasks;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Response;
using Tnf.Dto.Request;
using Tnf.Localization;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Enumerables;
using Tnf.Dto;
using System.Linq;

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

        public async Task<IResponseDto> GetAllPresidents(GetAllPresidentsDto request)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (request.PageSize <= 0)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(request.PageSize)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            return await _whiteHouserService.GetAllPresidents(request);
        }

        public async Task<IResponseDto> GetPresidentById(RequestDto<string> id)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (string.IsNullOrWhiteSpace(id.Id))
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(id.Id)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            return await _whiteHouserService.GetPresidentById(id);
        }

        public async Task<IResponseDto> InsertPresidentAsync(PresidentDto president, bool sync = true)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (president == null)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(president)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            return await _whiteHouserService.InsertPresidentAsync(president, sync);
        }

        public async Task<IResponseDto> UpdatePresidentAsync(string id, PresidentDto president)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (string.IsNullOrEmpty(id))
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(id)) });
            }

            if (president == null)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(president)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            president.Id = id;
            return await _whiteHouserService.UpdatePresidentAsync(president);
        }

        public async Task<IResponseDto> DeletePresidentAsync(string id)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (string.IsNullOrWhiteSpace(id))
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(id)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            return await _whiteHouserService.DeletePresidentAsync(id);
        }
    }
}
