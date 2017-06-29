using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.App.Domain.Services;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Events.Bus;
using Tnf.Architecture.Domain.Events.WhiteHouse;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.App.Dto.Response;
using Tnf.App.Dto.Request;
using Tnf.App.Bus.Notifications;

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

        public Task<ListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request) => Repository.GetAllPresidents(request);

        public async Task<PresidentDto> GetPresidentById(RequestDto<string> id)
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

        public async Task<PresidentDto> InsertPresidentAsync(PresidentDto dto, bool sync = false)
        {
            var builder = new PresidentBuilder(Notification)
               .WithId(dto.Id)
               .WithName(dto.Name)
               .WithAddress(dto.Address);

            var president = builder.Build();

            if (!Notification.HasNotification())
            {
                var ids = await Repository.InsertPresidentsAsync(new List<President>() { president }, sync);
                dto.Id = ids[0];

                // Trigger president created event
                _eventBus.Trigger(new PresidentCreatedEvent(president));
            }

            return dto;
        }

        public async Task DeletePresidentAsync(string id)
        {
            if (!(await Repository.DeletePresidentsAsync(id)))
            {
                Notification.Raise(NotificationEvent.DefaultBuilder
                                    .WithNotFoundStatus()
                                    .WithMessage(AppConsts.LocalizationSourceName, President.Error.CouldNotFindPresident)
                                    .Build());
            }
        }

        public async Task<PresidentDto> UpdatePresidentAsync(PresidentDto dto)
        {
            var presidentBuilder = new PresidentBuilder(Notification)
                .WithId(dto.Id)
                .WithName(dto.Name)
                .WithAddress(dto.Address);

            var president = presidentBuilder.Build();
            
            if (!Notification.HasNotification())
            {
                var data = await Repository.UpdatePresidentsAsync(president);

                if (data == null)
                {
                    Notification.Raise(NotificationEvent.DefaultBuilder
                                        .WithNotFoundStatus()
                                        .WithMessage(AppConsts.LocalizationSourceName, President.Error.CouldNotFindPresident)
                                        .Build());
                }
            }

            return dto;
        }
    }
}
