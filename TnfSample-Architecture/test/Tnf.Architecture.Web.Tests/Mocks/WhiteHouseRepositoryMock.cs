using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Carol.Entities;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.AutoMapper;
using Tnf.Domain.Repositories;

namespace Tnf.Architecture.Web.Tests.Mocks
{
    public class WhiteHouseRepositoryMock : IWhiteHouseRepository
    {
        private readonly ConcurrentDictionary<string, PresidentPoco> _presidents;

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
            var result = _presidents.TryRemove(id, out PresidentPoco _);

            return Task.FromResult(result);
        }

        public Task<President> GetPresidentById(RequestDto<string> requestDto)
        {
            _presidents.TryGetValue(requestDto.GetId(), out PresidentPoco presidentpoco);

            return Task.FromResult(presidentpoco.MapTo<President>());
        }

        public Task<string> InsertPresidentsAsync(President president)
        {
            _presidents.TryAdd(president.Id, president.MapTo<PresidentPoco>());
            
            return Task.FromResult(president.Id);
        }

        public Task<President> UpdatePresidentsAsync(President president)
        {
            var deleted = _presidents.TryRemove(president.Id, out PresidentPoco _);

            if (deleted)
                _presidents.TryAdd(president.Id, president.MapTo<PresidentPoco>());
            else
                president = null;

            return Task.FromResult(president);
        }
    }
}
