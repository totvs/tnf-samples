using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Events.Bus;
using Tnf.Architecture.Domain.Events.WhiteHouse;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Localization;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Response;
using Tnf.Dto.Request;

namespace Tnf.Architecture.Domain.WhiteHouse
{
    internal class WhiteHouseService : DomainService<IWhiteHouseRepository>, IWhiteHouseService
    {
        private readonly IEventBus _eventBus;

        public WhiteHouseService(IWhiteHouseRepository repository,
            IEventBus eventBus)
            : base(repository)
        {
            _eventBus = eventBus;
        }

        public Task<SuccessResponseListDto<PresidentDto>> GetAllPresidents(GetAllPresidentsDto request) => Repository.GetAllPresidents(request);

        public async Task<IResponseDto> GetPresidentById(RequestDto<string> id)
        {
            var builder = new Builder();

            var notificationMessage = LocalizationHelper.GetString(
                AppConsts.LocalizationSourceName,
                President.Error.CouldNotFindPresident);

            var president = await Repository.GetPresidentById(id);

            builder
                .WithNotFound()
                .WithNotFoundStatus()
                .IsTrue(president != null, President.Error.CouldNotFindPresident, notificationMessage);

            var response = builder.Build();

            if (response.Success)
                response = president;

            return response;
        }

        public async Task<IResponseDto> InsertPresidentAsync(PresidentDto dto, bool sync = false)
        {
            var builder = new PresidentBuilder()
               .WithInvalidPresident()
               .WithId(dto.Id)
               .WithName(dto.Name)
               .WithAddress(dto.Address);

            var response = builder.Build();

            if (response.Success)
            {
                var ids = await Repository.InsertPresidentsAsync(new List<President>() { builder.Instance }, sync);
                dto.Id = ids[0];

                response = dto;

                // Trigger president created event
                _eventBus.Trigger(new PresidentCreatedEvent(builder.Instance));
            }

            return response;
        }

        public async Task<IResponseDto> DeletePresidentAsync(string id)
        {
            var builder = new Builder();

            var notificationMessage = LocalizationHelper.GetString(
                AppConsts.LocalizationSourceName,
                President.Error.CouldNotFindPresident);

            builder
                .WithNotFound()
                .WithNotFoundStatus()
                .IsTrue(await Repository.DeletePresidentsAsync(id), President.Error.CouldNotFindPresident, notificationMessage);

            return builder.Build();
        }

        public async Task<IResponseDto> UpdatePresidentAsync(PresidentDto dto)
        {
            var presidentBuilder = new PresidentBuilder()
                .WithInvalidPresident()
                .WithId(dto.Id)
                .WithName(dto.Name)
                .WithAddress(dto.Address);

            var response = presidentBuilder.Build();

            if (response.Success)
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    President.Error.CouldNotFindPresident);
                
                var data = await Repository.UpdatePresidentsAsync(presidentBuilder.Instance);

                var builder = new Builder();

                builder
                    .WithNotFound()
                    .WithNotFoundStatus()
                    .IsTrue(data != null, President.Error.CouldNotFindPresident, notificationMessage);

                response = builder.Build();

                if (response.Success)
                    response = dto;
            }

            return response;
        }
    }
}
