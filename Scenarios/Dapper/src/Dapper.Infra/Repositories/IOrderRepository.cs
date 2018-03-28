using Dapper.Infra.Dto;
using Dapper.Infra.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace Dapper.Infra.Repositories
{
    public interface IOrderRepository : IRepository
    {
        Task<Customer> GetCustomerFromPurchaseOrder(int orderId);
        Task<PurchaseOrder> GetPurchaseOrder(RequestDto request);
        Task<SumarizedOrder> GetSumarizedPurchaseOrderFromDate(DateTime date);
        Task<IListDto<PurchaseOrderDto, int>> GetAllPurchaseOrders(SumarizedOrderRequestAllDto param);
    }
}
