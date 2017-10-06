using System.Threading.Tasks;
using Tnf.App.Bus.Notifications;
using Tnf.App.Domain.Services;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Common;
using Tnf.Architecture.Domain.Events.WhiteHouse;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Events.Bus;

namespace Tnf.Architecture.Domain.WhiteHouse
{
    internal class WhiteHouseService : AppDomainService, IWhiteHouseService
    {
        private readonly IWhiteHouseRepository _whiteHouseRepository;
        private readonly IEventBus _eventBus;

        public WhiteHouseService(
            IWhiteHouseRepository repository,
            IEventBus eventBus)
        {
            _whiteHouseRepository = repository;
            _eventBus = eventBus;
        }

        public async Task<President> GetPresidentById(IRequestDto<string> id)
        {
            var president = await _whiteHouseRepository.GetPresidentById(id);

            if (president == null)
            {
                Notification.Raise(Notification.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, President.Error.CouldNotFindPresident)
                                    .Build());
            }

            return president;
        }

        public async Task<string> InsertPresidentAsync(PresidentBuilder builder)
        {
            var president = builder.Build();

            if (Notification.HasNotification())
                return "";

            var id = await _whiteHouseRepository.InsertPresidentsAsync(president);

            // Trigger president created event
            _eventBus.Trigger(new PresidentCreatedEvent(president));

            return id;
        }

        public async Task DeletePresidentAsync(string id)
        {
            if (!await _whiteHouseRepository.DeletePresidentsAsync(id))
            {
                Notification.Raise(Notification.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, President.Error.CouldNotFindPresident)
                                    .Build());
            }
        }

        public async Task UpdatePresidentAsync(PresidentBuilder builder)
        {
            var president = builder.Build();

            if (Notification.HasNotification())
                return;

            var data = await _whiteHouseRepository.UpdatePresidentsAsync(president);

            if (data == null)
            {
                Notification.Raise(Notification.DefaultBuilder
                    .WithNotFoundStatus()
                    .WithMessage(AppConsts.LocalizationSourceName, President.Error.CouldNotFindPresident)
                    .Build());
            }
        }
    }
}
