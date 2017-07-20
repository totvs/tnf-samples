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
    internal class WhiteHouseService : AppDomainService<IWhiteHouseRepository>, IWhiteHouseService
    {
        private readonly IEventBus _eventBus;

        public WhiteHouseService(
            IWhiteHouseRepository repository,
            IEventBus eventBus)
            : base(repository)
        {
            _eventBus = eventBus;
        }

        public async Task<President> GetPresidentById(RequestDto<string> id)
        {
            var president = await Repository.GetPresidentById(id);

            if (president == null)
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
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

            var id = await Repository.InsertPresidentsAsync(president);

            // Trigger president created event
            _eventBus.Trigger(new PresidentCreatedEvent(president));

            return id;
        }

        public async Task DeletePresidentAsync(string id)
        {
            if (!await Repository.DeletePresidentsAsync(id))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
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

            var data = await Repository.UpdatePresidentsAsync(president);

            if (data == null)
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                    .WithNotFoundStatus()
                    .WithMessage(AppConsts.LocalizationSourceName, President.Error.CouldNotFindPresident)
                    .Build());
            }
        }
    }
}
