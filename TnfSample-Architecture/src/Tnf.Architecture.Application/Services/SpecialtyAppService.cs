using Tnf.App.Application.Services;
using Tnf.App.Bus.Notifications;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common;
using Tnf.Architecture.Common.Enumerables;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.ReadInterfaces;
using Tnf.AutoMapper;

namespace Tnf.Architecture.Application.Services
{
    public class SpecialtyAppService : AppApplicationService, ISpecialtyAppService
    {
        private readonly ISpecialtyService _service;
        private readonly ISpecialtyReadRepository _readRepository;

        public SpecialtyAppService(ISpecialtyService service, ISpecialtyReadRepository readRepository)
        {
            _service = service;
            _readRepository = readRepository;
        }

        public ListDto<SpecialtyDto, int> GetAllSpecialties(GetAllSpecialtiesDto request)
            => _readRepository.GetAllSpecialties(request);

        public SpecialtyDto GetSpecialty(RequestDto id)
        {
            if (id.GetId() <= 0)
                RaiseNotification(nameof(id));

            if (Notification.HasNotification())
                return new SpecialtyDto();

            var entity = _service.GetSpecialty(id);

            return entity.MapTo<SpecialtyDto>();
        }

        public SpecialtyDto CreateSpecialty(SpecialtyDto specialty)
        {
            if (specialty == null)
                RaiseNotification(nameof(specialty));

            if (Notification.HasNotification())
                return new SpecialtyDto();

            var specialtyBuilder = new SpecialtyBuilder(Notification)
                .WithId(specialty.Id)
                .WithDescription(specialty.Description);

            specialty.Id = _service.CreateSpecialty(specialtyBuilder);

            return specialty;
        }

        public SpecialtyDto UpdateSpecialty(int id, SpecialtyDto specialty)
        {
            if (id <= 0)
                RaiseNotification(nameof(id));

            if (specialty == null)
                RaiseNotification(nameof(specialty));

            if (Notification.HasNotification())
                return new SpecialtyDto();

            var specialtyBuilder = new SpecialtyBuilder(Notification)
                .WithId(id)
                .WithDescription(specialty.Description);

            _service.UpdateSpecialty(specialtyBuilder);

            specialty.Id = id;
            return specialty;
        }

        public void DeleteSpecialty(int id)
        {
            if (id <= 0)
                RaiseNotification(nameof(id));

            if (!Notification.HasNotification())
                _service.DeleteSpecialty(id);
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
