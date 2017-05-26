using Tnf.Localization;
using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Enumerables;
using System.Linq;

namespace Tnf.Architecture.Application.Services
{
    public class SpecialtyAppService : ApplicationService, ISpecialtyAppService
    {
        private readonly ISpecialtyService _service;

        public SpecialtyAppService(ISpecialtyService service)
        {
            _service = service;
        }

        public IResponseDto GetAllSpecialties(GetAllSpecialtiesDto request)
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

            return _service.GetAllSpecialties(request);
        }

        public IResponseDto GetSpecialty(RequestDto<int> id)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (id.Id <= 0)
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

            return _service.GetSpecialty(id);
        }

        public IResponseDto CreateSpecialty(SpecialtyDto specialty)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (specialty == null)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(specialty)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            return _service.CreateSpecialty(specialty);
        }

        public IResponseDto UpdateSpecialty(int id, SpecialtyDto specialty)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (id <= 0)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(id)) });
            }

            if (specialty == null)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Error.InvalidParameterDynamic);

                builder.WithNotification(new Notification() { Message = string.Format(notificationMessage, nameof(specialty)) });
            }

            if (builder.Notifications.Any())
                return builder
                        .FromEnum(Error.InvalidParameter)
                        .Build();

            specialty.Id = id;
            return _service.UpdateSpecialty(specialty);
        }

        public IResponseDto DeleteSpecialty(int id)
        {
            var builder = ErrorResponseDto.DefaultBuilder;

            if (id <= 0)
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

            return _service.DeleteSpecialty(id);
        }
    }
}
