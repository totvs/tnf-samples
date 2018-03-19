using Querying.Infra.Dto;
using Querying.Infra.Entities;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace Querying.Infra.Repositories
{
    public interface IOrderRepository : IRepository
    {
        Task<Customer> GetCustomerFromOrder(RequestDto request);
        Task<Order> GetOrder(RequestDto request);
        Task<SumarizedOrder> GetSumarizedOrderFromProduct(SumarizedOrderRequestAllDto param);
    }
}
