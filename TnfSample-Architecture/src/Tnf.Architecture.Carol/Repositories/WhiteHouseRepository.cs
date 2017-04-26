using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.App.Carol.Repositories;
using Tnf.Provider.Carol;
using System.Linq;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Data.Entities;

namespace Tnf.Architecture.Data.Repositories
{
    public class WhiteHouseRepository : CarolRepositoryBase<PresidentEntity>, IWhiteHouseRepository
    {
        public WhiteHouseRepository(ICarolClient client) :
            base(client)
        {
        }

        public async Task DeletePresidentsAsync(string id)
        {
            await DeleteAsync(id);
        }

        public async Task DeletePresidentsAsync(PresidentDto president)
        {
            await DeleteAsync(president.Id);
        }

        public async Task<PagingDtoResponse<PresidentDto>> GetAllPresidents(GellAllPresidentsRequestDto request)
        {
            var response = new PagingDtoResponse<PresidentDto>();

            var query = Client.Queries<PresidentEntity>().ProcessFilter()
                .Offset(request.Offset)
                .PageSize(request.PageSize)
                .IndexType(Provider.Carol.Messages.ProcessFilter.IndexType.STAGING)
                .MustList((m) => m.TypeFilter()
                                  .MatchFilter(p => p.Name, request.Name)
                                  .TermFilter(p => p.ZipCode, request.ZipCode));

            var resultData = await GetAllAsync(query);

            response.Count = resultData.Count;
            response.Took = resultData.Took;
            response.TotalHits = resultData.TotalHits;

            resultData.Hits.ForEach((item) => response.Data.Add(new PresidentDto(item.Id, item.Name, item.ZipCode)));

            return response;
        }

        public async Task<PresidentDto> GetPresidentById(string id)
        {
            var presidentData = await GetAsync(id);

            var president = new PresidentDto(presidentData.Id, presidentData.Name, presidentData.ZipCode);
            return president;
        }

        public async Task<List<PresidentDto>> InsertPresidentsAsync(List<PresidentDto> presidents, bool sync = false)
        {
            var presidentEntities = presidents.Select(s => new PresidentEntity()
            {
                Id = s.Id,
                Name = s.Name,
                ZipCode = s.ZipCode.Number
            }).ToList();

            presidentEntities = await InsertAsync(presidentEntities, sync) as List<PresidentEntity>;

            return presidents;
        }

        public async Task UpdatePresidentsAsync(PresidentDto president)
        {
            await UpdateAsync(new PresidentEntity()
            {
                Id = president.Id,
                Name = president.Name,
                ZipCode = president.ZipCode.Number
            });
        }
    }
}
