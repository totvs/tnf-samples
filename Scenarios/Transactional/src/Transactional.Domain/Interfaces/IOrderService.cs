using System.Collections.Generic;
using System.Threading.Tasks;
using Transactional.Domain.Entities;

namespace Transactional.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateNewOrder(Order order);
        List<Order> GetAllOrders();
        Task DeleteAsync(int id);
    }
}