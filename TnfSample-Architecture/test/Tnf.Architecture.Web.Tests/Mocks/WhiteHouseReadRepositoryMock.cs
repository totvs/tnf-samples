using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Carol.Entities;
using Tnf.Architecture.Carol.ReadInterfaces;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.AutoMapper;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Web.Tests.Mocks
{
    public class WhiteHouseReadRepositoryMock : IWhiteHouseReadRepository
    {
        private readonly ConcurrentDictionary<string, PresidentPoco> _presidents;

        public WhiteHouseReadRepositoryMock()
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

        public Task<ListDto<PresidentDto, string>> GetAllPresidents(GetAllPresidentsDto request)
        {
            var presidents = _presidents
                .Select(s => s.Value)
                .AsQueryable()
                .SkipAndTakeByRequestDto(request)
                .OrderByRequestDto(request)
                .ToList();

            var result = new ListDto<PresidentDto, string> { Items = presidents.MapTo<List<PresidentDto>>() };

            return Task.FromResult(result);
        }
    }
}
