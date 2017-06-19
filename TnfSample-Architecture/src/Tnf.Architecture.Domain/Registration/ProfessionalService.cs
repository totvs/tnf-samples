using Tnf.App.Bus.Notifications;
using Tnf.App.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Domain.Registration
{
    public class ProfessionalService : AppDomainService<IProfessionalRepository>, IProfessionalService
    {
        private readonly IProfessionalDapperRepository _repositoryDapper;

        public ProfessionalService(IProfessionalRepository repository, IProfessionalDapperRepository repositoryDapper)
            : base(repository)
        {
            _repositoryDapper = repositoryDapper;
        }

        public ListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request) => Repository.GetAllProfessionals(request);

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys)
        {
            ProfessionalDto dto = null;

            if (!Repository.ExistsProfessional(keys.GetId()))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Professional.Error.CouldNotFindProfessional)
                                    .Build());
            }

            if (!Notification.HasNotification())
                dto = Repository.GetProfessional(keys);

            return dto;
        }

        public ProfessionalDto CreateProfessional(ProfessionalDto dto)
        {
            var builder = new ProfessionalBuilder(Notification)
                   .WithProfessionalId(dto.ProfessionalId)
                   .WithCode(dto.Code)
                   .WithName(dto.Name)
                   .WithPhone(dto.Phone)
                   .WithEmail(dto.Email)
                   .WithAddress(dto.Address);

            var professional = builder.Build();

            if (!Notification.HasNotification())
            {
                var keys = Repository.CreateProfessional(professional);

                dto.ProfessionalId = keys.ProfessionalId;
                dto.Code = keys.Code;

                Repository.AddOrRemoveSpecialties(keys, dto.Specialties);
            }

            return dto;
        }

        public void DeleteProfessional(ProfessionalKeysDto keys)
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

        public ProfessionalDto UpdateProfessional(ProfessionalDto dto)
        {
            var professionalBuilder = new ProfessionalBuilder(Notification)
                   .WithProfessionalId(dto.ProfessionalId)
                   .WithCode(dto.Code)
                   .WithName(dto.Name)
                   .WithPhone(dto.Phone)
                   .WithEmail(dto.Email)
                   .WithAddress(dto.Address);

            var keys = new ProfessionalKeysDto(dto.ProfessionalId, dto.Code);

            var professional = professionalBuilder.Build();

            if (!Notification.HasNotification())
            {
                if (!Repository.ExistsProfessional(keys))
                {
                    Notification.Raise(NotificationEvent.DefaultBuilder
                                        .WithNotFoundStatus()
                                        .WithMessage(AppConsts.LocalizationSourceName, Professional.Error.CouldNotFindProfessional)
                                        .Build());
                }

                if (!Notification.HasNotification())
                {
                    Repository.UpdateProfessional(professional);
                    Repository.AddOrRemoveSpecialties(keys, dto.Specialties);
                }
            }

            return dto;
        }
    }
}
