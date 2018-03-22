using SuperMarket.Backoffice.Sales.Application.Services.Interfaces;
using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.Sales.Application.Services
{
    public class PurchaseOrderAppService : ApplicationService, IPurchaseOrderAppService
    {
        public readonly IPurchaseOrderService _domainService;
        public readonly IPurchaseOrderReadRepository _readRepository;
        public readonly IPriceTableRepository _priceTableRepository;
        public readonly IPurchaseOrderRepository _purchaseOrderDomainRepository;

        public PurchaseOrderAppService(INotificationHandler notification, 
            IPurchaseOrderService domainService, 
            IPurchaseOrderReadRepository readRepository,
            IPriceTableRepository priceTableRepository,
            IPurchaseOrderRepository purchaseOrderDomainRepository) 
            : base(notification)
        {
            _domainService = domainService;
            _readRepository = readRepository;
            _priceTableRepository = priceTableRepository;
            _purchaseOrderDomainRepository = purchaseOrderDomainRepository;
        }

        public async Task<PurchaseOrderDto> CreatePurchaseOrderAsync(PurchaseOrderDto dto)
        {
            if (!ValidateDto<PurchaseOrderDto, Guid>(dto))
                return PurchaseOrderDto.NullInstance;

            var priceTable = await _priceTableRepository.GetPriceTableAsync();

            var purchaseOrderBuilder = PurchaseOrder.New(Notification)
                .UpdatePriceTable(priceTable)
                .WithCustomer(dto.CustomerId)
                .WithDiscount(dto.Discount)
                .AddPurchaseOrderLines(lines =>
                {
                    foreach (var productDto in dto.Products)
                        lines.AddProduct(productDto.ProductId, productDto.Quantity);
                });

            var entity = await _domainService.NewPurchaseOrder(purchaseOrderBuilder);

            if (Notification.HasNotification())
                return PurchaseOrderDto.NullInstance;

            dto.Id = entity.Id;

            return dto;
        }

        public async Task<PurchaseOrderDto> GetPurchaseOrderAsync(IRequestDto<Guid> id)
        {
            if (!ValidateRequestDto<IRequestDto<Guid>, Guid>(id))
                return PurchaseOrderDto.NullInstance;

            return await _readRepository.GetPurchaseOrderAsync(id);
        }

        public async Task<IListDto<PurchaseOrderDto, Guid>> GetAllPurchaseOrderAsync(PurchaseOrderRequestAllDto request)
            => await _readRepository.GetAllPurchaseOrdersAsync(request);

        public async Task<PurchaseOrderDto> UpdatePurchaseOrderAsync(Guid id, PurchaseOrderDto dto)
        {
            if (!ValidateDtoAndId(dto, id))
                return PurchaseOrderDto.NullInstance;

            var priceTable = await _priceTableRepository.GetPriceTableAsync();
            var entity = await _purchaseOrderDomainRepository.GetPurchaseOrder(id);

            var purchaseOrderBuilder = PurchaseOrder.Update(Notification, entity)
                .UpdatePriceTable(priceTable)
                .WithDiscount(dto.Discount)
                .UpdatePurchaseOrderLines(lines =>
                {
                    foreach (var productDto in dto.Products)
                        lines.AddProduct(productDto.ProductId, productDto.Quantity);
                });

            await _domainService.UpdatePurchaseOrder(purchaseOrderBuilder);

            dto.Id = id;

            return dto;
        }
    }
}
