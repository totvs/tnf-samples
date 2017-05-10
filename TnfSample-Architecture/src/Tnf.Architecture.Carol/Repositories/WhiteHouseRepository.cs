using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.App.Carol.Repositories;
using Tnf.Provider.Carol;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Data.Entities;
using Tnf.AutoMapper;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Data.Repositories
{
    public class WhiteHouseRepository : CarolRepositoryBase<PresidentPoco>, IWhiteHouseRepository
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

        public async Task<PagingResponseDto<PresidentDto>> GetAllPresidents(GellAllPresidentsDto request)
        {
            var response = new PagingResponseDto<PresidentDto>();

            var query = Client.Queries<PresidentPoco>().ProcessFilter()
                .Offset(request.Offset)
                .PageSize(request.PageSize)
                .IndexType(Provider.Carol.Messages.ProcessFilter.IndexType.STAGING)
                .MustList((m) => m.TypeFilter()
                                  .MatchFilter(p => p.Name, request.Name)
                                  .TermFilter(p => p.Address.ZipCode.Number, request.ZipCode));

            var resultData = await GetAllAsync(query);

            response.Total = resultData.TotalHits;

            response.Data = resultData.Hits.MapTo<List<PresidentDto>>();

            return response;
        }

        public async Task<PresidentDto> GetPresidentById(string id)
        {
            var presidentData = await GetAsync(id);

            var president = presidentData.MapTo<PresidentDto>();

            return president;
        }

        public async Task<List<PresidentDto>> InsertPresidentsAsync(List<PresidentDto> presidents, bool sync = false)
        {
            var pocos = presidents.MapTo<List<PresidentPoco>>();

            var result = await InsertAsync(pocos, sync);

            return result.MapTo<List<PresidentDto>>();
        }

        public async Task UpdatePresidentsAsync(PresidentDto president)
        {
            var poco = president.MapTo<PresidentPoco>();
            await UpdateAsync(poco);
        }
    }
}
