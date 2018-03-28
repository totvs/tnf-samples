using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Tnf.Notifications;
using Tnf.Repositories.Uow;
using Transactional.Domain.Entities;
using Transactional.Domain.Interfaces;

namespace Transactional.Domain.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;
        private readonly IPurchaseOrderRepository purchaseOrderRepository;
        private readonly INotificationHandler notificationHandler;

        public PurchaseOrderService(
            IUnitOfWorkManager unitOfWorkManager,
            IPurchaseOrderRepository purchaseOrderRepository,
            INotificationHandler notificationHandler)
        {
            this.unitOfWorkManager = unitOfWorkManager;
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.notificationHandler = notificationHandler;
        }

        /// <summary>
        /// Transacões aninhadas:
        /// Caso você precise trabalhar com este modelo siga o exemplo abaixo onde uma transação é aberta (Método CheckDuplicateOrder linha 50)
        /// com o paramêtro TransactionScopeOption definido para RequiresNew (isso significa que uma nova transação será criada)
        /// </summary>
        public async Task<PurchaseOrder> CreateNewPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                await CheckDuplicateOrder(purchaseOrder);

                if (notificationHandler.HasNotification())
                    return purchaseOrder;

                purchaseOrder = await purchaseOrderRepository.CreateNewPurchaseOrder(purchaseOrder);

                await uow.CompleteAsync();
            }

            return purchaseOrder;
        }

        private async Task CheckDuplicateOrder(PurchaseOrder purchaseOrder)
        {
            using (unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                var duplicateOrder = await purchaseOrderRepository.CheckDuplicatePurchaseOrder(purchaseOrder);
                if (duplicateOrder != null)
                {
                    notificationHandler.DefaultBuilder
                        .AsSpecification()
                        .WithMessage(Constants.LocalizationSourceName, GlobalizationKey.DuplicatePurchaseOrder)
                        .WithMessageFormat(purchaseOrder.ClientId, purchaseOrder.Data)
                        .Raise();
                }
            }
        }

        /// <summary>
        /// Toda vez que for utilizado o UnitOfWorkManager sem passar nenhum parâmetro
        /// ele irá criar uma transação tendo como base as opções definidas no startup da 
        /// aplicação <see cref="Transactional.Web.Startup"/> linha 55
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                await purchaseOrderRepository.DeleteAsync(id);

                await uow.CompleteAsync();
            }
        }

        /// <summary>
        /// Quando for necessário especificar algum aspecto específico como timeout da transação, escopo
        /// ou isolation level você pode utilizar a classe <see cref="UnitOfWorkOptions"/> para definir algum parâmetro
        /// </summary>
        public List<PurchaseOrder> GetAllPurchaseOrders()
        {
            var options = new UnitOfWorkOptions()
            {
                Timeout = TimeSpan.FromSeconds(10),
                IsTransactional = false,
                IsolationLevel = IsolationLevel.ReadCommitted,
                Scope = TransactionScopeOption.Required
            };

            using (var uow = unitOfWorkManager.Begin(options))
            {
                return purchaseOrderRepository.GetAllPurchaseOrders();
            }
        }
    }
}
