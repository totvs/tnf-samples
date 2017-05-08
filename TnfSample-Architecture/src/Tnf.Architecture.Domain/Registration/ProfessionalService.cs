using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto;

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

        public void DeleteProfessional(ProfessionalKeysDto keys) => Repository.DeleteProfessional(keys);

        public ProfessionalDto GetProfessional(ProfessionalKeysDto keys) => Repository.GetProfessional(keys);

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
                response.AddNotifications(build.Notifications);

            if (response.Success)
            {
                response.Data = Repository.UpdateProfessional(entity);

                Repository.AddOrRemoveSpecialties(new ProfessionalKeysDto(response.Data.ProfessionalId, response.Data.Code), entity.Specialties);

                response.Data.Specialties = entity.Specialties;
            }

            return response;
        }

        public PagingResponseDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request) => _specialtyRepository.GetAllSpecialties(request);

        public SpecialtyDto GetSpecialty(int id) => _specialtyRepository.GetSpecialty(id);

        public void DeleteSpecialty(int id) => _specialtyRepository.DeleteSpecialty(id);

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

        public DtoResponseBase<SpecialtyDto> UpdateSpecialty(SpecialtyDto dto)
        {
            var response = new DtoResponseBase<SpecialtyDto>();

            var builder = new SpecialtyBuilder()
                   .WithDescription(dto.Description);

            var build = builder.Build();
            if (!build.Success)
                response.AddNotifications(build.Notifications);

            if (response.Success)
                response.Data = _specialtyRepository.UpdateSpecialty(dto);

            return response;
        }
    }
}
