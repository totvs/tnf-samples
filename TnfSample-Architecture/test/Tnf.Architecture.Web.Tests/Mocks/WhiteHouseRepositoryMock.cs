using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Dto;
using Tnf.Dto;
using System.Linq;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Domain.Repositories;
using Tnf.Dto.Response;
using Tnf.Dto.Request;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.AutoMapper;

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

        public Task<bool> DeletePresidentsAsync(string id)
        {
            var result = _presidents.TryRemove(id, out PresidentDto presidentDto);

            return Task.FromResult(result);
        }

        public Task<SuccessResponseListDto<PresidentDto>> GetAllPresidents(GetAllPresidentsDto request)
        {
            var presidents = _presidents
                .Select(s => s.Value)
                .AsQueryable()
                .SkipAndTakeByRequestDto(request)
                .OrderByRequestDto(request)
                .ToList();

            var result = new SuccessResponseListDto<PresidentDto>();
            result.Items = presidents;

            return Task.FromResult(result);
        }

        public Task<PresidentDto> GetPresidentById(RequestDto<string> requestDto)
        {
            _presidents.TryGetValue(requestDto.Key, out PresidentDto dto);

            return Task.FromResult(dto);
        }

        public Task<List<string>> InsertPresidentsAsync(List<President> presidents, bool sync = false)
        {
            foreach (var item in presidents)
                _presidents.TryAdd(item.Id, item.MapTo<PresidentDto>());

            var allInsertedDtos = _presidents.Select(s => s.Value).ToList();

            return Task.FromResult(allInsertedDtos.Select(p => p.Id).ToList());
        }

        public Task<President> UpdatePresidentsAsync(President president)
        {
            var deleted = _presidents.TryRemove(president.Id, out PresidentDto removedDto);

            if (deleted)
                _presidents.TryAdd(president.Id, president.MapTo<PresidentDto>());
            else
                president = null;

            return Task.FromResult(president);
        }
    }
}
