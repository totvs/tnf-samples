using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Localization;

namespace Tnf.Architecture.Domain.Registration
{
    public class ProfessionalService : DomainService<IProfessionalRepository>, IProfessionalService
    {
        public ProfessionalService(IProfessionalRepository repository)
            : base(repository)
        {
        }

        public PagingResponseDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request) => Repository.GetAllProfessionals(request);

        public ProfessionalDto GetProfessional(ProfessionalKeysDto keys) => Repository.GetProfessional(keys);

        public DtoResponseBase<ProfessionalDto> CreateProfessional(ProfessionalCreateDto entity)
        {
            var response = new DtoResponseBase<ProfessionalDto>();

            var builder = new ProfessionalBuilder()
                   .WithName(entity.Name)
                   .WithAddress(entity.Address)
                   .WithPhone(entity.Phone)
                   .WithEmail(entity.Email);

            var build = builder.Build();
            if (!build.Success)
                response.AddNotifications(build.Notifications);

            if (response.Success)
            {
                response.Data = Repository.CreateProfessional(entity);

                Repository.AddOrRemoveSpecialties(new ProfessionalKeysDto(response.Data.ProfessionalId, response.Data.Code), entity.Specialties);

                response.Data.Specialties = entity.Specialties;
            }

            return response;
        }

        public DtoResponseBase DeleteProfessional(ProfessionalKeysDto keys)
        {
            var result = new DtoResponseBase();

            var success = Repository.DeleteProfessional(keys);

            if (!success)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Professional.Error.CouldNotFindProfessional);

                result.AddNotification(new Notification(Professional.Error.CouldNotFindProfessional, notificationMessage));
            }

            return result;
        }

        public DtoResponseBase<ProfessionalDto> UpdateProfessional(ProfessionalDto entity)
        {
            var response = new DtoResponseBase<ProfessionalDto>();

            var builder = new ProfessionalBuilder()
                   .WithProfessionalId(entity.ProfessionalId)
                   .WithName(entity.Name)
                   .WithAddress(entity.Address)
                   .WithPhone(entity.Phone)
                   .WithEmail(entity.Email);

            var build = builder.Build();
            if (!build.Success)
            {
                response.AddNotifications(build.Notifications);
                response.Data = new ProfessionalDto();
            }

            if (response.Success)
            {
                if (Repository.ExistsProfessional(new ProfessionalKeysDto(entity.ProfessionalId, entity.Code)))
                {
                    response.Data = Repository.UpdateProfessional(entity);
                    Repository.AddOrRemoveSpecialties(new ProfessionalKeysDto(entity.ProfessionalId, entity.Code), entity.Specialties);

                    response.Data.Specialties = entity.Specialties;
                }
                else
                {
                    response.Data = null;
                    var notificationMessage = LocalizationHelper.GetString(
                        AppConsts.LocalizationSourceName,
                        Professional.Error.CouldNotFindProfessional);

                    response.AddNotification(new Notification(Professional.Error.CouldNotFindProfessional, notificationMessage));
                }
            }

            return response;
        }
    }
}
