using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMarket.Backoffice.FiscalService.Domain.Entities;
using SuperMarket.Backoffice.FiscalService.Infra.Dtos;
using SuperMarket.Backoffice.Sales.Infra.Queue.Messages;
using Tnf.Bus.Client;
using Tnf.Bus.Queue.Interfaces;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Repositories.Uow;

namespace SuperMarket.Backoffice.FiscalService.Web.Controllers
{
    [Route("api/fiscalservice")]
    public class FiscalServiceController : TnfController,
        ISubscribe<PurchaseOrderChangedMessage>,
        IPublish<TaxMovimentCalculatedMessage>
    {
        private readonly IDomainService<TaxMoviment, Guid> _domainService;
        private readonly ILogger<TaxMoviment> _logger;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public FiscalServiceController(
            ILogger<TaxMoviment> logger,
            IUnitOfWorkManager unitOfWorkManager,
            IDomainService<TaxMoviment, Guid> domainService)
        {
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
                .New(Notification)
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

                if (Notification.HasNotification())
                {
                    var messages = Notification
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
