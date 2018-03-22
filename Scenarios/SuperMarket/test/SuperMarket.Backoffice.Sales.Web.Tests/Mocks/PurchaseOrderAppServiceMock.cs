using SuperMarket.Backoffice.Sales.Application.Services.Interfaces;
using SuperMarket.Backoffice.Sales.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Sales.Web.Tests.Mocks
{
    public class PurchaseOrderAppServiceMock : IPurchaseOrderAppService
    {
        public static Guid purchaseOrderGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<PurchaseOrderDto> list = new List<PurchaseOrderDto>() {
            new PurchaseOrderDto() { Id = purchaseOrderGuid, CustomerId = Guid.NewGuid(), Discount = 10 },
            new PurchaseOrderDto() { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Discount = 20 },
            new PurchaseOrderDto() { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Discount = 30 }
        };

        public Task<PurchaseOrderDto> CreatePurchaseOrderAsync(PurchaseOrderDto dto)
        {
            if (dto == null)
                return Task.FromResult<PurchaseOrderDto>(null);

            dto.Id = Guid.NewGuid();
            list.Add(dto);

            return dto.AsTask();
        }

        public Task<PurchaseOrderDto> GetPurchaseOrderAsync(IRequestDto<Guid> id)
        {
            var dto = list.FirstOrDefault(c => c.Id == id.GetId());

            return dto.AsTask();
        }

        public Task<IListDto<PurchaseOrderDto, Guid>> GetAllPurchaseOrderAsync(PurchaseOrderRequestAllDto request)
        {
            IListDto<PurchaseOrderDto, Guid> result = new ListDto<PurchaseOrderDto, Guid> { HasNext = false, Items = list };

            return result.AsTask();
        }

        public Task<PurchaseOrderDto> UpdatePurchaseOrderAsync(Guid id, PurchaseOrderDto dto)
        {
            if (dto == null)
                return Task.FromResult<PurchaseOrderDto>(null);

            list.RemoveAll(c => c.Id == id);
            dto.Id = id;
            list.Add(dto);

            return dto.AsTask();
        }
    }
}
