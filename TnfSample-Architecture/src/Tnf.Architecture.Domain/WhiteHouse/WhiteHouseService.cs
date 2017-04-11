using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Domain.Events;
using Tnf.Events.Bus;

namespace Tnf.Architecture.Domain.WhiteHouse
{
    public class WhiteHouseService : DomainService<IWhiteHouseRepository>, IWhiteHouseService
    {
        private readonly IEventBus _eventBus;

        public WhiteHouseService(IWhiteHouseRepository repository,
            IEventBus eventBus)
            : base(repository)
        {
            _eventBus = eventBus;
        }

        public async Task<DtoResponseBase> DeletePresidentAsync(string id)
        {
            await Repository.DeletePresidentsAsync(id);

            return new DtoResponseBase();
        }

        public Task<PagingDtoResponse<PresidentDto>> GetAllPresidents(GellAllPresidentsRequestDto request)
        {
            return Repository.GetAllPresidents(request);
        }

        public async Task<PresidentDto> GetPresidentById(string id)
        {
            return await Repository.GetPresidentById(id);
        }

        public async Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> presidents, bool sync = false)
        {
            var response = new DtoResponseBase<List<PresidentDto>>();

            foreach (var item in presidents)
            {
                var builder = new PresidentBuilder()
                   .WithId(item.Id)
                   .WithName(item.Name)
                   .WithZipCode(item.ZipCode);

                var build = builder.Build();
                if (!build.Success)
                    response.AddNotifications(build.Notifications);
            }

            if (response.Success)
            {
                presidents = await Repository.InsertPresidentsAsync(presidents, sync);
                response.Data = presidents;

                // Trigger president created event
                presidents.ForEach((president) => _eventBus.Trigger(new PresidentCreatedEvent(president)));
            }

            return response;
        }

        public async Task<DtoResponseBase> UpdatePresidentAsync(PresidentDto president)
        {
            var builder = new PresidentBuilder()
                .WithId(president.Id)
                .WithName(president.Name)
                .WithZipCode(president.ZipCode);

            var response = builder.Build();
            if (response.Success)
            {
                await Repository.UpdatePresidentsAsync(president);
            }

            return response;
        }
    }
}
