using Tnf.App.Bus.Notifications;
using Tnf.App.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Common;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;

namespace Tnf.Architecture.Domain.Registration
{
    public class SpecialtyService : AppDomainService, ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyService(ISpecialtyRepository repository)
        {
            _specialtyRepository = repository;
        }

        public Specialty GetSpecialty(RequestDto requestDto)
        {
            if (!_specialtyRepository.ExistsSpecialty(requestDto.GetId()))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                                    .Build());

                return null;
            }

            return _specialtyRepository.GetSpecialty(requestDto);
        }

        public int CreateSpecialty(SpecialtyBuilder builder)
        {
            var specialty = builder.Build();

            return Notification.HasNotification() ? 0 : _specialtyRepository.CreateSpecialty(specialty);
        }

        public void DeleteSpecialty(int id)
        {
            if (!_specialtyRepository.ExistsSpecialty(id))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                                    .Build());
            }

            if (!Notification.HasNotification())
                _specialtyRepository.DeleteSpecialty(id);
        }

        public void UpdateSpecialty(SpecialtyBuilder builder)
        {
            var specialty = builder.Build();

            if (Notification.HasNotification())
                return;

            if (!_specialtyRepository.ExistsSpecialty(specialty.Id))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                    .WithNotFoundStatus()
                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                    .Build());
            }

            if (!Notification.HasNotification())
                _specialtyRepository.UpdateSpecialty(specialty);
        }
    }
}
