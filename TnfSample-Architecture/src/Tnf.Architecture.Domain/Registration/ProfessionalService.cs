using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;
using Tnf.Localization;

namespace Tnf.Architecture.Domain.Registration
{
    public class ProfessionalService : DomainService<IProfessionalRepository>, IProfessionalService
    {
        public ProfessionalService(IProfessionalRepository repository)
            : base(repository)
        {
        }

        public SuccessResponseListDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto request) => Repository.GetAllProfessionals(request);

        public ProfessionalDto GetProfessional(RequestDto<ProfessionalKeysDto> keys) => Repository.GetProfessional(keys);

        public IResponseDto CreateProfessional(ProfessionalDto dto)
        {
            var builder = new ProfessionalBuilder()
                   .WithName(dto.Name)
                   .WithAddress(dto.Address)
                   .WithPhone(dto.Phone)
                   .WithEmail(dto.Email);

            var response = builder.Build();

            if (response.Success)
            {
                var keys = Repository.CreateProfessional(builder.Instance);

                builder = new ProfessionalBuilder(builder.Instance)
                   .WithProfessionalId(keys.ProfessionalId)
                   .WithCode(keys.Code);

                Repository.AddOrRemoveSpecialties(keys, dto.Specialties);

                response = dto;
            }

            return response;
        }

        public IResponseDto DeleteProfessional(ProfessionalKeysDto keys)
        {
            var builder = new Builder();

            var notificationMessage = LocalizationHelper.GetString(
                AppConsts.LocalizationSourceName,
                Professional.Error.CouldNotFindProfessional);

            builder
                .IsTrue(Repository.ExistsProfessional(keys), Professional.Error.CouldNotFindProfessional, notificationMessage);

            var response = builder.Build();

            if (response.Success)
                Repository.DeleteProfessional(keys);

            return response;
        }

        public IResponseDto UpdateProfessional(ProfessionalDto dto)
        {
            var builder = new ProfessionalBuilder()
                   .WithProfessionalId(dto.ProfessionalId)
                   .WithName(dto.Name)
                   .WithAddress(dto.Address)
                   .WithPhone(dto.Phone)
                   .WithEmail(dto.Email);

            var build = builder.Build();

            var response = builder.Build();

            if (response.Success)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Professional.Error.CouldNotFindProfessional);

                builder = new ProfessionalBuilder(builder.Instance);

                builder
                    .IsTrue(Repository.ExistsProfessional(new ProfessionalKeysDto(dto.ProfessionalId, dto.Code)), Professional.Error.CouldNotFindProfessional, notificationMessage);

                response = builder.Build();
                
                if (response.Success)
                {
                    Repository.UpdateProfessional(builder.Instance);
                    Repository.AddOrRemoveSpecialties(new ProfessionalKeysDto(dto.ProfessionalId, dto.Code), dto.Specialties);
                    
                    response = dto;
                }
            }

            return response;
        }
    }
}
