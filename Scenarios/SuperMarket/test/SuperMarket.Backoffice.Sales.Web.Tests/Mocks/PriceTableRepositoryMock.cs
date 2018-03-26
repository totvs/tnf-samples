using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using System.Threading.Tasks;

namespace SuperMarket.Backoffice.Sales.Web.Tests.Mocks
{
    public class PriceTableRepositoryMock : IPriceTableRepository
    {
        private readonly PriceTableMock _priceTable;

        public PriceTableRepositoryMock(PriceTableMock priceTable)
        {
            _priceTable = priceTable;
        }

        public Task<PriceTable> GetPriceTableAsync()
            => _priceTable.priceTable.AsTask();
    }
}
