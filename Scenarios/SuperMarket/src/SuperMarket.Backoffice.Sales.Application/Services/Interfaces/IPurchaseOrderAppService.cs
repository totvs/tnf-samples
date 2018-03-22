using SuperMarket.Backoffice.Sales.Dto;
using System;
using Tnf.Application.Services;

namespace SuperMarket.Backoffice.Sales.Application.Services.Interfaces
{
    public interface IPurchaseOrderAppService : IAsyncApplicationService<PurchaseOrderDto, PurchaseOrderRequestAllDto, Guid>
    {
    }
}
