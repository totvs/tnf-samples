using SuperMarket.Backoffice.Crud.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Backoffice.Crud.Web.Tests.Mocks
{
    public class PriceTableRepositoryMock : IPriceTableRepository
    {
        public Task<Dictionary<Guid, decimal>> GetPriceTable()
            => new Dictionary<Guid, decimal>
            {
                { Guid.NewGuid(), 1 },
                { Guid.NewGuid(), 2 },
                { Guid.NewGuid(), 3 },
                { Guid.NewGuid(), 4 }
            }.AsTask();
    }
}
