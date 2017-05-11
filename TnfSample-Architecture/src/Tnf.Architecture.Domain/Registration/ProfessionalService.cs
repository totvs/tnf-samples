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
        private readonly ISpecialtyRepository _specialtyRepository;

        public ProfessionalService(IProfessionalRepository repository, ISpecialtyRepository professionalSpecialtyRepository)
            : base(repository)
        {
            _specialtyRepository = professionalSpecialtyRepository;
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



        public PagingResponseDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request) => _specialtyRepository.GetAllSpecialties(request);

        public SpecialtyDto GetSpecialty(int id)
        {
            SpecialtyDto result = null;

            if (_specialtyRepository.ExistsSpecialty(id))
                result = _specialtyRepository.GetSpecialty(id);

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
                response.Data = _specialtyRepository.CreateSpecialty(dto);

            return response;
        }

        public DtoResponseBase DeleteSpecialty(int id)
        {
            var result = new DtoResponseBase();

            if (_specialtyRepository.ExistsSpecialty(id))
            {
                _specialtyRepository.DeleteSpecialty(id);
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
                if (_specialtyRepository.ExistsSpecialty(dto.Id))
                {
                    response.Data = _specialtyRepository.UpdateSpecialty(dto);
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
