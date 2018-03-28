using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMarket.FiscalService.Domain.Entities;
using SuperMarket.FiscalService.Infra.Dtos;
using Tnf.Bus.Client;
using Tnf.Bus.Queue.Interfaces;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Notifications;
using Tnf.Repositories.Uow;
using SuperMarket.FiscalService.Infra.Queue.Messages;

namespace SuperMarket.FiscalService.Web.Controllers
{
    [Route("api/fiscalservice")]
    public class FiscalServiceController : TnfController,
        ISubscribe<PurchaseOrderChangedMessage>,
        IPublish<TaxMovimentCalculatedMessage>
    {
        private readonly IDomainService<TaxMoviment, Guid> _domainService;
        private readonly INotificationHandler _notificationHandler;
        private readonly ILogger<TaxMoviment> _logger;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public FiscalServiceController(
            INotificationHandler notificationHandler,
            ILogger<TaxMoviment> logger,
            IUnitOfWorkManager unitOfWorkManager,
            IDomainService<TaxMoviment, Guid> domainService)
        {
            _notificationHandler = notificationHandler;
            _logger = logger;
            _unitOfWorkManager = unitOfWorkManager;
            _domainService = domainService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]RequestAllDto requestAll)
        {
            var options = new UnitOfWorkOptions()
            {
                IsTransactional = false,
                Scope = TransactionScopeOption.Suppress
            };

            using (var uow = _unitOfWorkManager.Begin(options))
            {
                var response = await _domainService.GetAllAsync<TaxMovimentDto>(requestAll);

                return CreateResponseOnGetAll(response);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task Handle(PurchaseOrderChangedMessage message)
        {
            try
            {
                var movimentBuilder = TaxMoviment
                    .New(_notificationHandler)
                    .ForPurchaseOrder(
                        message.PurchaseOrderId,
                        message.PurchaseOrderBaseValue,
                        message.PurchaseOrderDiscount);

                var options = new UnitOfWorkOptions()
                {
                    IsTransactional = true,
                    Scope = TransactionScopeOption.Required,
                    IsolationLevel = IsolationLevel.ReadCommitted
                };

                using (var uow = _unitOfWorkManager.Begin(options))
                {
                    await _domainService.InsertAndGetIdAsync(movimentBuilder);

                    await uow.CompleteAsync();
                }

                if (_notificationHandler.HasNotification())
                {
                    var messages = _notificationHandler
                        .GetAll()
                        .Select(s => s.Message)
                        .JoinAsString(", ");

                    _logger.LogInformation($"Tax Moviment process found error {messages}");
                    return;
                }

                var moviment = movimentBuilder.Build();

                await Handle(new TaxMovimentCalculatedMessage()
                {
                    PurchaseOrderId = moviment.PurchaseOrderId,
                    Tax = moviment.Tax,
                    TotalValue = moviment.PurchaseOrderTotalValue
                });

                message.DoAck();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handle PurchaseOrderChangedMessage");
                throw;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Task Handle(TaxMovimentCalculatedMessage message)
            => message.Publish();
    }
}
