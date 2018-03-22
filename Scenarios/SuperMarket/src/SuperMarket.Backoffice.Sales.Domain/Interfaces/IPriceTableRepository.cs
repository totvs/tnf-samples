using SuperMarket.Backoffice.Sales.Domain.Entities;
using System.Threading.Tasks;
using Tnf.Repositories;

namespace SuperMarket.Backoffice.Sales.Domain.Interfaces
{
    public interface IPriceTableRepository : IRepository
    {
        Task<PriceTable> GetPriceTableAsync();
    }
}
