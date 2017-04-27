using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Sample.Core.Interfaces.Repositories;
using Tnf.Sample.Core.Interfaces.Services;
using Tnf.Sample.Dto;

namespace Tnf.Sample.Core.WhiteHouse
{
    public class WhiteHouseService : DomainService<IWhiteHouseRepository>, IWhiteHouseService
    {
        private readonly IWhiteHouseRepository _repository;

        public WhiteHouseService(IWhiteHouseRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<DtoResponseBase> DeletePresidentAsync(string id)
        {
            await _repository.DeletePresidentsAsync(id);

            return new DtoResponseBase();
        }

        public Task<PagingDtoResponse<PresidentDto>> GetAllPresidents(GellAllPresidentsRequestDto request)
        {
            return _repository.GetAllPresidents(request);
        }

        public async Task<PresidentDto> GetPresidentById(string id)
        {
            return await _repository.GetPresidentById(id);
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
                presidents = await _repository.InsertPresidentsAsync(presidents, sync);
                response.Data = presidents;
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
                await _repository.UpdatePresidentsAsync(president);
            }

            return response;
        }
    }
}
