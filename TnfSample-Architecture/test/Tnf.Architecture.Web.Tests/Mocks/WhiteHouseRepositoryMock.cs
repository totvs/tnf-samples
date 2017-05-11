using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Dto;
using Tnf.Dto;
using System.Linq;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Web.Tests.Mocks
{
    public class WhiteHouseRepositoryMock : IWhiteHouseRepository
    {
        private readonly ConcurrentDictionary<string, PresidentDto> _presidents = null;

        public WhiteHouseRepositoryMock()
        {
            var source = new Dictionary<string, PresidentDto>()
            {
                { "1", new PresidentDto("1", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))) },
                { "2", new PresidentDto("2", "Bill Clinton", new Address("Rua de teste", "123", "APT 12", new ZipCode("87654321"))) },
                { "3", new PresidentDto("3", "Donald Trump", new Address("Rua de teste", "123", "APT 12", new ZipCode("12341234"))) },
                { "4", new PresidentDto("4", "Thomas Jefferson", new Address("Rua de teste", "123", "APT 12", new ZipCode("56785678"))) },
                { "5", new PresidentDto("5", "Abraham Lincoln", new Address("Rua de teste", "123", "APT 12", new ZipCode("14785236"))) },
                { "6", new PresidentDto("6", "Ronald Reagan", new Address("Rua de teste", "123", "APT 12", new ZipCode("85236417"))) }
            };

            _presidents = new ConcurrentDictionary<string, PresidentDto>(source);
        }

        public Task<bool> DeletePresidentsAsync(PresidentDto president)
        {
            var result = _presidents.TryRemove(president.Id, out PresidentDto presidentDto);

            return Task.FromResult(result);
        }

        public Task<bool> DeletePresidentsAsync(string id)
        {
            var result = _presidents.TryRemove(id, out PresidentDto presidentDto);

            return Task.FromResult(result);
        }

        public Task<PagingResponseDto<PresidentDto>> GetAllPresidents(GetAllPresidentsDto request)
        {
            var presidents = _presidents.Where(w =>
                string.IsNullOrWhiteSpace(request.Name) || request.Name.Contains(w.Value.Name) &&
                string.IsNullOrWhiteSpace(request.ZipCode) || request.ZipCode.Contains(w.Value.Address.ZipCode.Number))
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

        public Task<PresidentDto> UpdatePresidentsAsync(PresidentDto president)
        {
            var deleted = _presidents.TryRemove(president.Id, out PresidentDto removedDto);

            if (deleted)
                _presidents.TryAdd(president.Id, president);
            else
                president = null;

            return Task.FromResult(president);
        }
    }
}
