using System;
using Tnf.App.Application.Services;
using Tnf.App.Bus.Notifications;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Enumerables;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Services
{
    public class ProfessionalAppService : AppApplicationService, IProfessionalAppService
    {
        private readonly IProfessionalService _service;

        public ProfessionalAppService(IProfessionalService service)
        {
            _service = service;
        }

        public ListDto<ProfessionalDto, ProfessionalKeysDto> GetAllProfessionals(GetAllProfessionalsDto request)
            => _service.GetAllProfessionals(request);

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys)
        {
            var professionalId = keys.GetId().ProfessionalId;
            var code = keys.GetId().Code;

            if (keys.GetId().ProfessionalId <= 0)
                RaiseNotification(nameof(professionalId));

            if (keys.GetId().Code == Guid.Empty)
                RaiseNotification(nameof(code));

            return Notification.HasNotification() ? new ProfessionalDto() : _service.GetProfessional(keys);
        }

        public ProfessionalDto CreateProfessional(ProfessionalDto professional)
        {
            if (professional == null)
                RaiseNotification(nameof(professional));

            return Notification.HasNotification() ? new ProfessionalDto() : _service.CreateProfessional(professional);
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
