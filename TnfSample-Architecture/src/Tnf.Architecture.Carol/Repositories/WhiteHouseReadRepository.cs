using System.Threading.Tasks;
using Tnf.App.Carol.Repositories;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Carol.Entities;
using Tnf.Architecture.Carol.ReadInterfaces;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Provider.Carol;

namespace Tnf.Architecture.Carol.Repositories
{
    public class WhiteHouseReadRepository : CarolRepositoryBase<PresidentPoco>, IWhiteHouseReadRepository
    {
        public WhiteHouseReadRepository(ICarolClient client) : base(client)
        {
        }

        public async Task<ListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request)
        {
            var query = Client.Query<PresidentPoco>().ProcessFilter()
                .SkipAndTakeByRequestDto(request)
                .OrderByRequestDto(request)
                .IndexType(Provider.Carol.Messages.ProcessFilter.IndexType.STAGING)
                .MustList((m) => m.TypeFilter()
                                  .MatchFilter(p => p.Name, request.Name)
                                  .TermFilter(p => p.Address.ZipCode.Number, request.ZipCode));

            var resultData = await GetAllAsync(query);

            return resultData.ToListDto<PresidentPoco, PresidentDto, string>(request);
        }
    }
}
