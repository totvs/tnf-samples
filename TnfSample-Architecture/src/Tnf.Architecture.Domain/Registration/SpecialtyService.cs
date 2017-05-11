using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Localization;

namespace Tnf.Architecture.Domain.Registration
{
    public class SpecialtyService : DomainService<ISpecialtyRepository>, ISpecialtyService
    {
        public SpecialtyService(ISpecialtyRepository repository)
            : base(repository)
        {
        }

        public PagingResponseDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request) => Repository.GetAllSpecialties(request);

        public SpecialtyDto GetSpecialty(int id)
        {
            SpecialtyDto result = null;

            if (Repository.ExistsSpecialty(id))
                result = Repository.GetSpecialty(id);

            return result;
        }

        public DtoResponseBase<SpecialtyDto> CreateSpecialty(SpecialtyDto dto)
        {
            var response = new DtoResponseBase<SpecialtyDto>();

            var builder = new SpecialtyBuilder()
                   .WithDescription(dto.Description);

            var build = builder.Build();
            if (!build.Success)
                response.AddNotifications(build.Notifications);

            if (response.Success)
                response.Data = Repository.CreateSpecialty(dto);

            return response;
        }

        public DtoResponseBase DeleteSpecialty(int id)
        {
            var result = new DtoResponseBase();

            if (Repository.ExistsSpecialty(id))
            {
                Repository.DeleteSpecialty(id);
            }
            else
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Specialty.Error.CouldNotFindSpecialty);

                result.AddNotification(new Notification(Specialty.Error.CouldNotFindSpecialty, notificationMessage));
            }

            return result;
        }

        public DtoResponseBase<SpecialtyDto> UpdateSpecialty(SpecialtyDto dto)
        {
            var response = new DtoResponseBase<SpecialtyDto>();

            var builder = new SpecialtyBuilder()
                   .WithDescription(dto.Description);

            var build = builder.Build();
            if (!build.Success)
            {
                response.AddNotifications(build.Notifications);
                response.Data = new SpecialtyDto();
            }

            if (response.Success)
            {
                if (Repository.ExistsSpecialty(dto.Id))
                {
                    response.Data = Repository.UpdateSpecialty(dto);
                }
                else
                {
                    response.Data = null;
                    var notificationMessage = LocalizationHelper.GetString(
                        AppConsts.LocalizationSourceName,
                        Specialty.Error.CouldNotFindSpecialty);

                    response.AddNotification(new Notification(Specialty.Error.CouldNotFindSpecialty, notificationMessage));
                }
            }

            return response;
        }
    }
}
