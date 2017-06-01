using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.App.Carol.Repositories;
using Tnf.Provider.Carol;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Data.Entities;
using Tnf.AutoMapper;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Dto.Response;
using Tnf.Dto.Request;
using Tnf.Architecture.Domain.WhiteHouse;
using System.Linq;

namespace Tnf.Architecture.Data.Repositories
{
    public class WhiteHouseRepository : CarolRepositoryBase<PresidentPoco>, IWhiteHouseRepository
    {
        public WhiteHouseRepository(ICarolClient client) :
            base(client)
        {
        }

        public async Task<bool> DeletePresidentsAsync(string id)
        {
            return await DeleteAsync(id);
        }

        public async Task<SuccessResponseListDto<PresidentDto>> GetAllPresidents(GetAllPresidentsDto request)
        {
            var response = new SuccessResponseListDto<PresidentDto>();

            var query = Client.Queries<PresidentPoco>().ProcessFilter()
                .SkipAndTakeByRequestDto(request)
                .OrderByRequestDto(request)
                .IndexType(Provider.Carol.Messages.ProcessFilter.IndexType.STAGING)
                .MustList((m) => m.TypeFilter()
                                  .MatchFilter(p => p.Name, request.Name)
                                  .TermFilter(p => p.Address.ZipCode.Number, request.ZipCode));

            var resultData = await GetAllAsync(query);

            response.Total = resultData.TotalHits;
            response.Items = resultData.Hits.MapTo<List<PresidentDto>>();
            response.HasNext = resultData.TotalHits > ((request.Page - 1) * request.PageSize) + response.Items.Count();

            return response;
        }

        public async Task<PresidentDto> GetPresidentById(RequestDto<string> requestDto)
        {
            var presidentData = await GetAsync(requestDto.GetId());

            var president = presidentData.MapTo<PresidentDto>();

            return president;
        }

        public async Task<List<string>> InsertPresidentsAsync(List<President> presidents, bool sync = false)
        {
            var pocos = presidents.MapTo<List<PresidentPoco>>();

            var result = await InsertAsync(pocos, sync);

            return result.Select(p => p.Id).ToList();
        }

        public async Task<President> UpdatePresidentsAsync(President president)
        {
            var poco = president.MapTo<PresidentPoco>();
            var result = await UpdateAsync(poco);

            return president;
        }
    }
}
