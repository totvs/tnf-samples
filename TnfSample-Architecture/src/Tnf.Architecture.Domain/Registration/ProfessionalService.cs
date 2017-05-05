using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Paging;
using Tnf.Architecture.Dto.Registration;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace Tnf.Architecture.Domain.Registration
{
    public class ProfessionalService : DomainService<IProfessionalRepository>, IProfessionalService
    {
        public ProfessionalService(IProfessionalRepository repository)
            : base(repository)
        {
        }

        public PagingResponseDto<ProfessionalDto> All(GetAllProfessionalsDto request) => Repository.All(request);

        public DtoResponseBase<ProfessionalDto> Create(ProfessionalCreateDto entity)
        {
            var response = new DtoResponseBase<ProfessionalDto>();

            var builder = new ProfessionalBuilder()
                   .WithName(entity.Name)
                   .WithAddress(entity.Address, entity.AddressNumber, entity.AddressComplement, entity.ZipCode)
                   .WithPhone(entity.Phone)
                   .WithEmail(entity.Email);

            var build = builder.Build();
            if (!build.Success)
                response.AddNotifications(build.Notifications);

            if (response.Success)
                response.Data = Repository.Insert(entity);

            return response;
        }

        public void Delete(ProfessionalKeysDto keys) => Repository.Delete(keys);

        public ProfessionalDto Get(ProfessionalKeysDto keys) => Repository.Get(keys);

        public DtoResponseBase<ProfessionalDto> Update(ProfessionalDto entity)
        {
            var response = new DtoResponseBase<ProfessionalDto>();

            var builder = new ProfessionalBuilder()
                   .WithProfessionalId(entity.ProfessionalId)
                   .WithName(entity.Name)
                   .WithAddress(entity.Address, entity.AddressNumber, entity.AddressComplement, entity.ZipCode)
                   .WithPhone(entity.Phone)
                   .WithEmail(entity.Email);

            var build = builder.Build();
            if (!build.Success)
                response.AddNotifications(build.Notifications);

            if (response.Success)
                response.Data = Repository.Update(entity);

            return response;
        }
    }
}
