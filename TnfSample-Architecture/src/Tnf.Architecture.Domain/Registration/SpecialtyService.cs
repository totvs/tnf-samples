using System.Threading.Tasks;
using Tnf.App.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Common;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;

namespace Tnf.Architecture.Domain.Registration
{
    public class SpecialtyService : AppDomainService, ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyService(ISpecialtyRepository repository)
        {
            _specialtyRepository = repository;
        }

        public async Task<Specialty> GetSpecialty(IRequestDto requestDto)
        {
            if (!await _specialtyRepository.ExistsSpecialty(requestDto.GetId()).ForAwait())
            {
                Notification.Raise(Notification.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                                    .Build());

                return null;
            }

            return await _specialtyRepository.GetSpecialty(requestDto).ForAwait();
        }

        public async Task<int> CreateSpecialty(SpecialtyBuilder builder)
        {
            var specialty = builder.Build();

            return Notification.HasNotification() ? 0 : await _specialtyRepository.CreateSpecialty(specialty).ForAwait();
        }

        public async Task DeleteSpecialty(int id)
        {
            if (!await _specialtyRepository.ExistsSpecialty(id).ForAwait())
            {
                Notification.Raise(Notification.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                                    .Build());
            }

            if (!Notification.HasNotification())
                await _specialtyRepository.DeleteSpecialty(id).ForAwait();
        }

        public async Task UpdateSpecialty(SpecialtyBuilder builder)
        {
            var specialty = builder.Build();

            if (Notification.HasNotification())
                return;

            if (!await _specialtyRepository.ExistsSpecialty(specialty.Id).ForAwait())
            {
                Notification.Raise(Notification.DefaultBuilder
                    .WithNotFoundStatus()
                    .WithMessage(AppConsts.LocalizationSourceName, Specialty.Error.CouldNotFindSpecialty)
                    .Build());
            }

            if (!Notification.HasNotification())
                await _specialtyRepository.UpdateSpecialty(specialty).ForAwait();
        }
    }
}
