using Tnf.App.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.App.Dto.Response;
using Tnf.App.Dto.Request;
using System;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Enumerables;
using Tnf.App.Bus.Notifications;

namespace Tnf.Architecture.Application.Services
{
    public class ProfessionalAppService : AppApplicationService, IProfessionalAppService
    {
        private readonly IProfessionalService _service;

        public ProfessionalAppService(IProfessionalService service)
        {
            _service = service;
        }

        public ListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request)
        {
            if (request.PageSize <= 0)
                RaiseNotification(nameof(request.PageSize));

            if (Notification.HasNotification())
                return new ListDto<ProfessionalDto>();

            return _service.GetAllProfessionals(request);
        }

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys)
        {
            var professionalId = keys.GetId().ProfessionalId;
            var code = keys.GetId().Code;

            if (keys.GetId().ProfessionalId <= 0)
                RaiseNotification(nameof(professionalId));

            if (keys.GetId().Code == Guid.Empty)
                RaiseNotification(nameof(code));

            if (Notification.HasNotification())
                return new ProfessionalDto();

            return _service.GetProfessional(keys);
        }

        public ProfessionalDto CreateProfessional(ProfessionalDto professional)
        {
            if (professional == null)
                RaiseNotification(nameof(professional));

            if (Notification.HasNotification())
                return new ProfessionalDto();

            return _service.CreateProfessional(professional);
        }

        public ProfessionalDto UpdateProfessional(ProfessionalKeysDto keys, ProfessionalDto professional)
        {
            if (keys.ProfessionalId <= 0)
                RaiseNotification(nameof(keys.ProfessionalId));

            if (keys.Code == Guid.Empty)
                RaiseNotification(nameof(keys.Code));

            if (professional == null)
                RaiseNotification(nameof(professional));

            if (Notification.HasNotification())
                return new ProfessionalDto();

            professional.ProfessionalId = keys.ProfessionalId;
            professional.Code = keys.Code;
            return _service.UpdateProfessional(professional);
        }

        public void DeleteProfessional(ProfessionalKeysDto keys)
        {
            if (keys.ProfessionalId <= 0)
                RaiseNotification(nameof(keys.ProfessionalId));

            if (keys.Code == Guid.Empty)
                RaiseNotification(nameof(keys.Code));

            if (!Notification.HasNotification())
                _service.DeleteProfessional(keys);
        }

        private void RaiseNotification(params string[] parameter)
        {
            Notification.Raise(NotificationEvent.DefaultBuilder
                                                .WithMessage(AppConsts.LocalizationSourceName, Error.InvalidParameter)
                                                .WithDetailedMessage(AppConsts.LocalizationSourceName, Error.InvalidParameterDynamic)
                                                .WithMessageFormat(parameter)
                                                .Build());
        }
    }
}
