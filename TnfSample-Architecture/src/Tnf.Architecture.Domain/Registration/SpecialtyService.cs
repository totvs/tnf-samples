using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.App.Bus.Notifications;

namespace Tnf.Architecture.Domain.Registration
{
    public class SpecialtyService : DomainService<ISpecialtyRepository>, ISpecialtyService
    {
        public SpecialtyService(ISpecialtyRepository repository)
            : base(repository)
        {
        }

        public ListDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request) => Repository.GetAllSpecialties(request);

        public SpecialtyDto GetSpecialty(RequestDto<int> requestDto)
        {
            SpecialtyDto dto = null;

            if (!Repository.ExistsSpecialty(requestDto.GetId()))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                                    .Build());
            }

            if (!Notification.HasNotification())
                dto = Repository.GetSpecialty(requestDto);

            return dto;
        }

        public SpecialtyDto CreateSpecialty(SpecialtyDto dto)
        {
            var builder = new SpecialtyBuilder()
                   .WithId(dto.Id)
                   .WithDescription(dto.Description);

            var specialty = builder.Build();

            if (!Notification.HasNotification())
                dto.Id = Repository.CreateSpecialty(specialty);

            return dto;
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

        public SpecialtyDto UpdateSpecialty(SpecialtyDto dto)
        {
            var specialtyBuilder = new SpecialtyBuilder()
                   .WithId(dto.Id)
                   .WithDescription(dto.Description);

            var specialty = specialtyBuilder.Build();

            if (!Notification.HasNotification())
            {
                if (!Repository.ExistsSpecialty(dto.Id))
                {
                    Notification.Raise(NotificationEvent.DefaultBuilder
                                        .WithNotFoundStatus()
                                        .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                                        .Build());
                }

                if (!Notification.HasNotification())
                    Repository.UpdateSpecialty(specialty);
            }

            return dto;
        }
    }
}
