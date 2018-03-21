using SuperMarket.Backoffice.Sales.Domain.Entities;
using Tnf.Repositories;

namespace SuperMarket.Backoffice.Sales.Domain.Interfaces
{
    public interface IPriceTableRepository : IRepository
    {
        PriceTable GetPriceTable();
    }
}
