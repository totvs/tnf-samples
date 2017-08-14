using System;
using Tnf.App.Bus.Notifications;
using Tnf.App.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Common;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;

namespace Tnf.Architecture.Domain.Registration
{
    public class ProfessionalService : AppDomainService, IProfessionalService
    {
        private readonly IProfessionalRepository _professionalRepository;

        public ProfessionalService(IProfessionalRepository repository)
        {
            _professionalRepository = repository;
        }

        public Professional GetProfessional(RequestDto<ComposeKey<Guid, decimal>> keys)
        {
            if (!_professionalRepository.ExistsProfessional(keys.GetId()))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Professional.Error.CouldNotFindProfessional)
                                    .Build());

                return null;
            }

            return _professionalRepository.GetProfessional(keys);
        }

        public ComposeKey<Guid, decimal> CreateProfessional(ProfessionalBuilder builder)
        {
            var professional = builder.Build();

            if (Notification.HasNotification())
                return new ComposeKey<Guid, decimal>();

            var keys = _professionalRepository.CreateProfessional(professional);

            _professionalRepository.AddOrRemoveSpecialties(keys, professional.Specialties);

            return keys;
        }

        public void DeleteProfessional(ComposeKey<Guid, decimal> keys)
        {
            if (!_professionalRepository.ExistsProfessional(keys))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Professional.Error.CouldNotFindProfessional)
                                    .Build());
            }

            if (!Notification.HasNotification())
                _professionalRepository.DeleteProfessional(keys);
        }

        public void UpdateProfessional(ProfessionalBuilder builder)
        {
            var professional = builder.Build();

            if (Notification.HasNotification())
                return;

            var keys = new ComposeKey<Guid, decimal>(professional.Code, professional.ProfessionalId);

            if (!_professionalRepository.ExistsProfessional(keys))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                    .WithNotFoundStatus()
                    .WithMessage(AppConsts.LocalizationSourceName, Professional.Error.CouldNotFindProfessional)
                    .Build());
            }

            if (Notification.HasNotification())
                return;

            _professionalRepository.UpdateProfessional(professional);
            _professionalRepository.AddOrRemoveSpecialties(keys, professional.Specialties);
        }
    }
}
