using Dapper.Infra.Dto;
using Dapper.Infra.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace Dapper.Infra.Repositories
{
    public interface IPurchaseOrderRepository : IRepository
    {
        Task<Customer> GetCustomerFromPurchaseOrder(int orderId);
        Task<PurchaseOrder> GetPurchaseOrder(RequestDto request);
        Task<SumarizedPurchaseOrder> GetSumarizedPurchaseOrderFromDate(DateTime date);
        Task<IListDto<PurchaseOrderDto, int>> GetAllPurchaseOrders(SumarizedPurchaseOrderRequestAllDto param);
    }
}
