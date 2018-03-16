using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Repositories;
using Transactional.Domain.Entities;

namespace Transactional.Domain.Interfaces
{
    public interface IOrderRepository : IRepository
    {
        Task<Order> CheckDuplicateOrder(Order order);
        Task<Order> CreateNewOrder(Order order);
        List<Order> GetAllOrders();
        Task DeleteAsync(int id);
    }
}
