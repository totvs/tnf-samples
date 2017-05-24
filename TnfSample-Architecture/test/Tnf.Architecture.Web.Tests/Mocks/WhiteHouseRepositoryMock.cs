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
using Tnf.Architecture.Data.Entities;

namespace Tnf.Architecture.Web.Tests.Mocks
{
    public class WhiteHouseRepositoryMock : IWhiteHouseRepository
    {
        private readonly ConcurrentDictionary<string, PresidentPoco> _presidents = null;

        public WhiteHouseRepositoryMock()
        {
            var source = new Dictionary<string, PresidentPoco>()
            {
                { "1", new PresidentPoco(){ Id="1", Name="George Washington", Address=new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678")) } },
                { "2", new PresidentPoco(){ Id="2", Name="Bill Clinton", Address=new Address("Rua de teste", "123", "APT 12", new ZipCode("87654321")) } },
                { "3", new PresidentPoco(){ Id="3", Name="Donald Trump", Address=new Address("Rua de teste", "123", "APT 12", new ZipCode("12341234")) } },
                { "4", new PresidentPoco(){ Id="4", Name="Thomas Jefferson", Address=new Address("Rua de teste", "123", "APT 12", new ZipCode("56785678")) } },
                { "5", new PresidentPoco(){ Id="5", Name="Abraham Lincoln", Address=new Address("Rua de teste", "123", "APT 12", new ZipCode("14785236")) } },
                { "6", new PresidentPoco(){ Id="6", Name="Ronald Reagan", Address=new Address("Rua de teste", "123", "APT 12", new ZipCode("85236417")) } },
            };

            _presidents = new ConcurrentDictionary<string, PresidentPoco>(source);
        }

        public Task<bool> DeletePresidentsAsync(string id)
        {
            var result = _presidents.TryRemove(id, out PresidentPoco presidentpoco);

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
            result.Items = presidents.MapTo<List<PresidentDto>>();

            return Task.FromResult(result);
        }

        public Task<PresidentDto> GetPresidentById(RequestDto<string> requestDto)
        {
            _presidents.TryGetValue(requestDto.Key, out PresidentPoco presidentpoco);

            return Task.FromResult(presidentpoco.MapTo<PresidentDto>());
        }

        public Task<List<string>> InsertPresidentsAsync(List<President> presidents, bool sync = false)
        {
            foreach (var item in presidents)
                _presidents.TryAdd(item.Id, item.MapTo<PresidentPoco>());

            var allInsertedDtos = _presidents.Select(s => s.Value).ToList();

            return Task.FromResult(allInsertedDtos.Select(p => p.Id).ToList());
        }

        public Task<President> UpdatePresidentsAsync(President president)
        {
            var deleted = _presidents.TryRemove(president.Id, out PresidentPoco removedPoco);

            if (deleted)
                _presidents.TryAdd(president.Id, president.MapTo<PresidentPoco>());
            else
                president = null;

            return Task.FromResult(president);
        }
    }
}
