using Tnf.App.Bus.Notifications;
using Tnf.App.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Common;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;

namespace Tnf.Architecture.Domain.Registration
{
    public class SpecialtyService : AppDomainService<ISpecialtyRepository>, ISpecialtyService
    {
        public SpecialtyService(ISpecialtyRepository repository)
            : base(repository)
        {
        }
        
        public Specialty GetSpecialty(RequestDto requestDto)
        {
            if (!Repository.ExistsSpecialty(requestDto.GetId()))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                                    .Build());

                return null;
            }

            return Repository.GetSpecialty(requestDto);
        }

        public int CreateSpecialty(SpecialtyBuilder builder)
        {
            var specialty = builder.Build();

            return Notification.HasNotification() ? 0 : Repository.CreateSpecialty(specialty);
        }

        public void DeleteSpecialty(int id)
        {
            if (!Repository.ExistsSpecialty(id))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                                    .Build());
            }

            if (!Notification.HasNotification())
                Repository.DeleteSpecialty(id);
        }

        public void UpdateSpecialty(SpecialtyBuilder builder)
        {
            var specialty = builder.Build();

            if (Notification.HasNotification())
                return;

            if (!Repository.ExistsSpecialty(specialty.Id))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                    .WithNotFoundStatus()
                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                    .Build());
            }

            if (!Notification.HasNotification())
                Repository.UpdateSpecialty(specialty);
        }
    }
}
