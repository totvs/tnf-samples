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
    public class SpecialtyService : DomainService<ISpecialtyRepository>, ISpecialtyService
    {
        public SpecialtyService(ISpecialtyRepository repository)
            : base(repository)
        {
        }

        public SuccessResponseListDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request) => Repository.GetAllSpecialties(request);

        public SpecialtyDto GetSpecialty(RequestDto<int> requestDto)
        {
            SpecialtyDto result = null;

            if (Repository.ExistsSpecialty(requestDto.Key))
                result = Repository.GetSpecialty(requestDto);

            return result;
        }

        public IResponseDto CreateSpecialty(SpecialtyDto dto)
        {
            var builder = new SpecialtyBuilder()
                   .WithDescription(dto.Description);

            var response = builder.Build();

            if (response.Success)
            {
                dto.Id = Repository.CreateSpecialty(builder.Instance);
                response = dto;
            }

            return response;
        }

        public IResponseDto DeleteSpecialty(int id)
        {
            var builder = new Builder();

            var notificationMessage = LocalizationHelper.GetString(
                AppConsts.LocalizationSourceName,
                Specialty.Error.CouldNotFindSpecialty);

            builder
                .IsTrue(Repository.ExistsSpecialty(id), Specialty.Error.CouldNotFindSpecialty, notificationMessage);

            var response = builder.Build();

            if (response.Success)
                Repository.DeleteSpecialty(id);

            return response;
        }

        public IResponseDto UpdateSpecialty(SpecialtyDto dto)
        {
            var builder = new SpecialtyBuilder()
                   .WithDescription(dto.Description);

            var response = builder.Build();

            if (response.Success)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    Specialty.Error.CouldNotFindSpecialty);

                builder = new SpecialtyBuilder(builder.Instance);

                builder
                    .IsTrue(Repository.ExistsSpecialty(dto.Id), Specialty.Error.CouldNotFindSpecialty, notificationMessage);

                response = builder.Build();

                if (response.Success)
                {
                    Repository.UpdateSpecialty(builder.Instance);
                    response = dto;
                }
            }

            return response;
        }
    }
}
