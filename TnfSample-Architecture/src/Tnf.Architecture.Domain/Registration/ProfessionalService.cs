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
    public class ProfessionalService : AppDomainService<IProfessionalRepository>, IProfessionalService
    {
        //private readonly IProfessionalDapperRepository _repositoryDapper;

        public ProfessionalService(IProfessionalRepository repository)
            : base(repository)
        {
            //_repositoryDapper = repositoryDapper;
        }

        public Professional GetProfessional(RequestDto<ComposeKey<Guid, decimal>> keys)
        {
            if (!Repository.ExistsProfessional(keys.GetId()))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Professional.Error.CouldNotFindProfessional)
                                    .Build());

                return null;
            }

            return Repository.GetProfessional(keys);
        }

        public ComposeKey<Guid, decimal> CreateProfessional(ProfessionalBuilder builder)
        {
            var professional = builder.Build();

            if (Notification.HasNotification())
                return new ComposeKey<Guid, decimal>();

            var keys = Repository.CreateProfessional(professional);

            Repository.AddOrRemoveSpecialties(keys, professional.Specialties);

            return keys;
        }

        public void DeleteProfessional(ComposeKey<Guid, decimal> keys)
        {
            if (!Repository.ExistsProfessional(keys))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Professional.Error.CouldNotFindProfessional)
                                    .Build());
            }

            if (!Notification.HasNotification())
                Repository.DeleteProfessional(keys);
        }

        public void UpdateProfessional(ProfessionalBuilder builder)
        {
            var professional = builder.Build();

            var keys = new ComposeKey<Guid, decimal>(professional.Code, professional.ProfessionalId);

            if (Notification.HasNotification())
                return;

            if (!Repository.ExistsProfessional(keys))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                    .WithNotFoundStatus()
                    .WithMessage(AppConsts.LocalizationSourceName, Professional.Error.CouldNotFindProfessional)
                    .Build());
            }

            if (Notification.HasNotification())
                return;

            Repository.UpdateProfessional(professional);
            Repository.AddOrRemoveSpecialties(keys, professional.Specialties);
        }
    }
}
