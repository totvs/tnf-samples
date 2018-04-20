using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.Sales.Application.Tests.Mocks
{
    public class PurchaseOrderReadRepositoryMock : IPurchaseOrderReadRepository
    {
        private readonly PurchaseOrderServiceMockManager _manager;

        public PurchaseOrderReadRepositoryMock(PurchaseOrderServiceMockManager manager)
        {
            _manager = manager;
        }

        public Task<IListDto<PurchaseOrderDto>> GetAllPurchaseOrdersAsync(PurchaseOrderRequestAllDto request)
        {
            var dtoList = _manager.List.Select(MapToPurchaseOrderDto);

            IListDto<PurchaseOrderDto> list = new ListDto<PurchaseOrderDto> { Items = dtoList.ToList(), HasNext = false };

            return list.AsTask();
        }

        public Task<PurchaseOrderDto> GetPurchaseOrderAsync(DefaultRequestDto key)
        {
            var entity = _manager.List.FirstOrDefault(c => c.Id == key.Id);

            return entity == null ? Task.FromResult<PurchaseOrderDto>(null) : MapToPurchaseOrderDto(entity).AsTask();
        }

        private PurchaseOrderDto MapToPurchaseOrderDto(PurchaseOrder purchaseOrder)
            => new PurchaseOrderDto
            {
                Id = purchaseOrder.Id,
                CustomerId = purchaseOrder.CustomerId,
                Discount = purchaseOrder.Discount,
                Products = purchaseOrder.Lines.Select(l => new PurchaseOrderDto.ProductDto(l.ProductId, l.Quantity)).ToList()
            };
    }
}
