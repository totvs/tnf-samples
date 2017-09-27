using Tnf.App.Application.Enums;
using Tnf.App.Application.Services;
using Tnf.App.Bus.Client;
using Tnf.App.Bus.Queue.Interfaces;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Application.Commands;
using Tnf.Architecture.Application.Events;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.ReadInterfaces;
using Tnf.AutoMapper;

namespace Tnf.Architecture.Application.Services
{
    public class SpecialtyAppService : AppApplicationService, ISpecialtyAppService,
        ISubscribe<SpecialtyCreateCommand>,
        IPublish<SpecialtyCreatedEvent>
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
            ValidateId(id);

            if (Notification.HasNotification())
                return SpecialtyDto.NullInstance;

            if (id.GetId() <= 0)
                RaiseNotification(TnfAppApplicationErrors.AppApplicationOnInvalidIdError);

            if (Notification.HasNotification())
                return SpecialtyDto.NullInstance;

            var entity = _service.GetSpecialty(id);

            return entity.MapTo<SpecialtyDto>();
        }

        public SpecialtyDto CreateSpecialty(SpecialtyDto specialty)
        {
            ValidateDto(specialty);

            if (Notification.HasNotification())
                return SpecialtyDto.NullInstance;

            var specialtyBuilder = new SpecialtyBuilder(Notification)
                .WithId(specialty.Id)
                .WithDescription(specialty.Description);

            specialty.Id = _service.CreateSpecialty(specialtyBuilder);

            return specialty;
        }

        public SpecialtyDto UpdateSpecialty(int id, SpecialtyDto specialty)
        {
            ValidateId(id);
            ValidateDto(specialty);

            if (id <= 0)
                RaiseNotification(TnfAppApplicationErrors.AppApplicationOnInvalidIdError);

            if (Notification.HasNotification())
                return SpecialtyDto.NullInstance;

            var specialtyBuilder = new SpecialtyBuilder(Notification)
                .WithId(id)
                .WithDescription(specialty.Description);

            _service.UpdateSpecialty(specialtyBuilder);

            specialty.Id = id;
            return specialty;
        }

        public void DeleteSpecialty(int id)
        {
            ValidateId(id);

            if (id <= 0)
                RaiseNotification(TnfAppApplicationErrors.AppApplicationOnInvalidIdError);

            if (Notification.HasNotification())
                return;

            _service.DeleteSpecialty(id);
        }

        public void Handle(SpecialtyCreateCommand message)
        {
            var dto = CreateSpecialty(new SpecialtyDto() { Description = message.Description });

            Handle(new SpecialtyCreatedEvent() { SpecialtyId = dto.Id });
        }

        public void Handle(SpecialtyCreatedEvent message) => message.Publish();
    }
}
