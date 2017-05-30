using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Response;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using System;
using Tnf.Localization;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Enumerables;
using Tnf.Dto;
using Tnf.Runtime.Validation;
using System.Linq;

namespace Tnf.Architecture.Application.Services
{
    [DisableValidation]
    public class ProfessionalAppService : ApplicationService, IProfessionalAppService
    {
        private readonly IProfessionalService _service;

        public ProfessionalAppService(IProfessionalService service)
        {
            _service = service;
        }

        public IResponseDto GetAllProfessionals(GetAllProfessionalsDto request)
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

            return _service.GetAllProfessionals(request);
        }

        public IResponseDto GetProfessional(RequestDto<ProfessionalKeysDto> keys)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (keys.Id.ProfessionalId <= 0)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(keys.Id.ProfessionalId)) });
            }

            if (keys.Id.Code == Guid.Empty)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(keys.Id.Code)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            return _service.GetProfessional(keys);
        }

        public IResponseDto CreateProfessional(ProfessionalDto professional)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (professional == null)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(professional)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            return _service.CreateProfessional(professional);
        }

        public IResponseDto UpdateProfessional(ProfessionalKeysDto keys, ProfessionalDto professional)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (keys.ProfessionalId <= 0)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(keys.ProfessionalId)) });
            }

            if (keys.Code == Guid.Empty)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(keys.Code)) });
            }

            if (professional == null)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(professional)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            professional.ProfessionalId = keys.ProfessionalId;
            professional.Code = keys.Code;
            return _service.UpdateProfessional(professional);
        }

        public IResponseDto DeleteProfessional(ProfessionalKeysDto keys)
        {
            var builder = ErrorResponseDto.DefaultBuilder;
            
            if (keys.ProfessionalId <= 0)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(keys.ProfessionalId)) });
            }

            if (keys.Code == Guid.Empty)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(keys.Code)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            return _service.DeleteProfessional(keys);
        }
    }
}
