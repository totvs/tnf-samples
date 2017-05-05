using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Dto;
using Tnf.Dto;
using System.Linq;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Web.Tests.Mocks
{
    public class WhiteHouseRepositoryMock : IWhiteHouseRepository
    {
        private readonly ConcurrentDictionary<string, PresidentDto> _presidents = null;

        public WhiteHouseRepositoryMock()
        {
            var source = new Dictionary<string, PresidentDto>()
            {
                { "1", new PresidentDto("1", "George Washington", "12345678") },
                { "2", new PresidentDto("2", "Bill Clinton", "87654321") },
                { "3", new PresidentDto("3", "Donald Trump", "12341234") },
                { "4", new PresidentDto("4", "Thomas Jefferson", "56785678") },
                { "5", new PresidentDto("5", "Abraham Lincoln", "14785236") },
                { "6", new PresidentDto("6", "Ronald Reagan", "85236417") }
            };

            _presidents = new ConcurrentDictionary<string, PresidentDto>(source);
        }

        public Task<DtoResponseBase> DeletePresidentAsync(string id)
        {
            _presidents.TryRemove(id, out PresidentDto presidentDto);

            return Task.FromResult(new DtoResponseBase());
        }

        public Task DeletePresidentsAsync(PresidentDto president)
        {
            _presidents.TryRemove(president.Id, out PresidentDto presidentDto);

            return Task.FromResult<object>(null);
        }

        public Task DeletePresidentsAsync(string id)
        {
            _presidents.TryRemove(id, out PresidentDto presidentDto);
            
            return Task.FromResult<object>(null);
        }

        public Task<PagingResponseDto<PresidentDto>> GetAllPresidents(GellAllPresidentsDto request)
        {
            var presidents = _presidents.Where(w =>
                string.IsNullOrWhiteSpace(request.Name) || request.Name.Contains(w.Value.Name) &&
                string.IsNullOrWhiteSpace(request.ZipCode) || request.ZipCode.Contains(w.Value.ZipCode.Number))
                .Select(s => s.Value)
                .ToList();

            return Task.FromResult(new PagingResponseDto<PresidentDto>(presidents));
        }

        public Task<PresidentDto> GetPresidentById(string id)
        {
            _presidents.TryGetValue(id, out PresidentDto dto);

            return Task.FromResult(dto);
        }

        public Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> dtos, bool sync)
        {
            foreach (var item in dtos)
                _presidents.TryAdd(item.Id, item);

            var allInsertedDtos = dtos.Select(s => s).ToList();

            return Task.FromResult(new DtoResponseBase<List<PresidentDto>>()
            {
                Data = allInsertedDtos
            });
        }

        public Task<List<PresidentDto>> InsertPresidentsAsync(List<PresidentDto> presidents, bool sync = false)
        {
            foreach (var item in presidents)
                _presidents.TryAdd(item.Id, item);

            var allInsertedDtos = _presidents.Select(s => s.Value).ToList();

            return Task.FromResult(allInsertedDtos);
        }

        public Task UpdatePresidentsAsync(PresidentDto president)
        {
            var deleted = _presidents.TryRemove(president.Id, out PresidentDto removedDto);
            if (deleted)
                _presidents.TryAdd(president.Id, president);

            return Task.FromResult<object>(null);
        }
    }
}
