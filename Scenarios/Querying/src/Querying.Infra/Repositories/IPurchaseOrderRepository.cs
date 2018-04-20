using Querying.Infra.Dto;
using Querying.Infra.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace Querying.Infra.Repositories
{
    public interface IPurchaseOrderRepository : IRepository
    {
        Task<Customer> GetCustomerFromPurchaseOrder(DefaultRequestDto request);
        Task<PurchaseOrder> GetPurchaseOrder(DefaultRequestDto request);
        Task<SumarizedPurchaseOrder> GetSumarizedPurchaseOrderFromDate(DateTime date);
    }
}
