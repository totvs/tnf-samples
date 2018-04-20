using SuperMarket.Backoffice.Sales.Application.Services.Interfaces;
using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Infra.Queue.Messages;
using SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using System.Transactions;
using Tnf.Application.Services;
using Tnf.Bus.Client;
using Tnf.Bus.Queue.Interfaces;
using Tnf.Dto;
using Tnf.Notifications;
using Tnf.Repositories.Uow;

namespace SuperMarket.Backoffice.Sales.Application.Services
{
    public class PurchaseOrderAppService : ApplicationService,
        IPurchaseOrderAppService,
        IPublish<PurchaseOrderChangedMessage>,
        ISubscribe<TaxMovimentCalculatedMessage>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public readonly IPurchaseOrderService _domainService;
        public readonly IPurchaseOrderReadRepository _readRepository;
        public readonly IPriceTableRepository _priceTableRepository;
        public readonly IPurchaseOrderRepository _purchaseOrderDomainRepository;

        public PurchaseOrderAppService(
            IUnitOfWorkManager unitOfWorkManager,
            INotificationHandler notification,
            IPurchaseOrderService domainService,
            IPurchaseOrderReadRepository readRepository,
            IPriceTableRepository priceTableRepository,
            IPurchaseOrderRepository purchaseOrderDomainRepository)
            : base(notification)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _domainService = domainService;
            _readRepository = readRepository;
            _priceTableRepository = priceTableRepository;
            _purchaseOrderDomainRepository = purchaseOrderDomainRepository;
        }

        public async Task<PurchaseOrderDto> CreatePurchaseOrderAsync(PurchaseOrderDto dto)
        {
            if (!ValidateDto(dto))
                return null;

            var options = new UnitOfWorkOptions()
            {
                IsTransactional = true,
                Scope = TransactionScopeOption.Required,
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var uow = _unitOfWorkManager.Begin(options))
            {
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

                var purchaseOrder = await _domainService.NewPurchaseOrder(purchaseOrderBuilder);

                if (Notification.HasNotification())
                    return null;

                await uow.CompleteAsync();

                dto.Id = purchaseOrder.Id;
                
                // Handler message after create purchase order for recalculate tax moviment
                await Handle(new PurchaseOrderChangedMessage()
                {
                    PurchaseOrderId = purchaseOrder.Id,
                    PurchaseOrderBaseValue = purchaseOrder.BaseValue,
                    PurchaseOrderDiscount = purchaseOrder.Discount
                });
            }

            return dto;
        }

        public async Task<PurchaseOrderDto> GetPurchaseOrderAsync(DefaultRequestDto id)
        {
            if (!ValidateRequestDto(id) || !ValidateId(id.Id))
                return null;

            var options = new UnitOfWorkOptions()
            {
                IsTransactional = false,
                Scope = TransactionScopeOption.Suppress
            };

            using (var uow = _unitOfWorkManager.Begin(options))
            {
                return await _readRepository.GetPurchaseOrderAsync(id);
            }
        }

        public async Task<IListDto<PurchaseOrderDto>> GetAllPurchaseOrderAsync(PurchaseOrderRequestAllDto request)
        {
            var options = new UnitOfWorkOptions()
            {
                IsTransactional = false,
                Scope = TransactionScopeOption.Suppress
            };

            using (var uow = _unitOfWorkManager.Begin(options))
            {
                return await _readRepository.GetAllPurchaseOrdersAsync(request);
            }
        }

        public async Task<PurchaseOrderDto> UpdatePurchaseOrderAsync(Guid id, PurchaseOrderDto dto)
        {
            if (!ValidateDtoAndId(dto, id))
                return null;

            PriceTable priceTable = null;
            PurchaseOrder purchaseOrderToUpdate = null;

            var options = new UnitOfWorkOptions()
            {
                IsTransactional = false,
                Scope = TransactionScopeOption.Suppress
            };

            using (var uow = _unitOfWorkManager.Begin(options))
            {
                priceTable = await _priceTableRepository.GetPriceTableAsync();

                purchaseOrderToUpdate = await _purchaseOrderDomainRepository.GetPurchaseOrder(id);
            }

            options = new UnitOfWorkOptions()
            {
                IsTransactional = true,
                Scope = TransactionScopeOption.Required,
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var uow = _unitOfWorkManager.Begin(options))
            {
                var updatePurchaseOrderBuilder = PurchaseOrder.Update(Notification, purchaseOrderToUpdate)
                    .UpdatePriceTable(priceTable)
                    .WithDiscount(dto.Discount)
                    .UpdatePurchaseOrderLines(lines =>
                    {
                        foreach (var productDto in dto.Products)
                            lines.AddProduct(productDto.ProductId, productDto.Quantity);
                    });

                purchaseOrderToUpdate = await _domainService.UpdatePurchaseOrder(updatePurchaseOrderBuilder);

                if (Notification.HasNotification())
                    return dto;

                await uow.CompleteAsync();
            }

            dto.Id = id;

            // Handler message after create purchase order for recalculate tax moviment
            await Handle(new PurchaseOrderChangedMessage()
            {
                PurchaseOrderId = purchaseOrderToUpdate.Id,
                PurchaseOrderBaseValue = purchaseOrderToUpdate.BaseValue,
                PurchaseOrderDiscount = purchaseOrderToUpdate.Discount
            });

            return dto;
        }

        public Task Handle(PurchaseOrderChangedMessage message) => message.Publish();

        // Process result of tax moviment service
        public async Task Handle(TaxMovimentCalculatedMessage message)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                await _domainService.UpdateTaxMoviment(
                    message.PurchaseOrderId,
                    message.Tax,
                    message.TotalValue);

                await uow.CompleteAsync();
            }

            // Manual Ack
            message.DoAck();
        }
    }
}
