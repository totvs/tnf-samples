using System.Threading.Tasks;
using Tnf.App.Application.Enums;
using Tnf.App.Application.Services;
using Tnf.App.AutoMapper;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.ReadInterfaces;

namespace Tnf.Architecture.Application.Services
{
    public class SpecialtyAppService : AppApplicationService, ISpecialtyAppService
    {
        private readonly ISpecialtyService _service;
        private readonly ISpecialtyReadRepository _readRepository;

        public SpecialtyAppService(ISpecialtyService service, ISpecialtyReadRepository readRepository)
        {
            _service = service;
            _readRepository = readRepository;
        }

        public async Task<IListDto<SpecialtyDto, int>> GetAllSpecialties(GetAllSpecialtiesDto request)
            => await _readRepository.GetAllSpecialties(request).ForAwait();

        public async Task<SpecialtyDto> GetSpecialty(IRequestDto id)
        {
            ValidateId(id);

            if (Notification.HasNotification())
                return SpecialtyDto.NullInstance;

            if (id.GetId() <= 0)
                RaiseNotification(TnfAppApplicationErrors.AppApplicationOnInvalidIdError);

            if (Notification.HasNotification())
                return SpecialtyDto.NullInstance;

            var entity = await _service.GetSpecialty(id).ForAwait();

            return entity.MapTo<SpecialtyDto>();
        }

        public async Task<SpecialtyDto> CreateSpecialty(SpecialtyDto specialty)
        {
            ValidateDto(specialty);

            if (Notification.HasNotification())
                return SpecialtyDto.NullInstance;

            var specialtyBuilder = new SpecialtyBuilder(Notification)
                .WithId(specialty.Id)
                .WithDescription(specialty.Description);

            specialty.Id = await _service.CreateSpecialty(specialtyBuilder).ForAwait();

            return specialty;
        }

        public async Task<SpecialtyDto> UpdateSpecialty(int id, SpecialtyDto specialty)
        {
            ValidateId(id);
            ValidateDto(specialty);

            if (id <= 0)
                RaiseNotification(TnfAppApplicationErrors.AppApplicationOnInvalidIdError);

            if (Notification.HasNotification())
                return SpecialtyDto.NullInstance;

            var specialtyBuilder = new SpecialtyBuilder(Notification)
                .WithId(id)
                .WithDescription(specialty.Description);

            await _service.UpdateSpecialty(specialtyBuilder).ForAwait();

            specialty.Id = id;
            return specialty;
        }

        public async Task DeleteSpecialty(int id)
        {
            ValidateId(id);

            if (id <= 0)
                RaiseNotification(TnfAppApplicationErrors.AppApplicationOnInvalidIdError);

            if (Notification.HasNotification())
                return;

            await _service.DeleteSpecialty(id).ForAwait();
        }
    }
}
