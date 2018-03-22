using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using System.Threading.Tasks;

namespace SuperMarket.Backoffice.Sales.Application.Tests.Mocks
{
    public class PriceTableRepositoryMock : IPriceTableRepository
    {
        private readonly PurchaseOrderServiceMockManager _manager;

        public PriceTableRepositoryMock(PurchaseOrderServiceMockManager manager)
        {
            _manager = manager;
        }

        public Task<PriceTable> GetPriceTableAsync()
            => _manager.PriceTable.AsTask();
    }
}
