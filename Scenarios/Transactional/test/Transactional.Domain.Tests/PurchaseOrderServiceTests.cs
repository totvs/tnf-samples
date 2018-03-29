using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.TestBase;
using Tnf.Localization;
using Transacional.Domain;
using Transactional.Domain.Entities;
using Transactional.Domain.Interfaces;
using Transactional.Infra.Context;
using Xunit;

namespace Transactional.Domain.Tests
{
    public class PurchaseOrderServiceTests : TnfEfCoreIntegratedTestBase
    {
        private ILocalizationManager localizationManager;
        private PurchaseOrder purchaseOrder = null;

        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            services
                .AddDomainDependency()                                  // dependencia da camada Transactional.Domain
                .AddTnfEfCoreSqliteInMemory()                           // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<PurchaseOrderContext>();     // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore
        }

        protected override void PostInitialize(IServiceProvider provider)
        {
            base.PostInitialize(provider);

            provider.ConfigureTnf().ConfigureLocalization();

            localizationManager = provider.GetService<ILocalizationManager>();

            // Arrange
            provider.UsingDbContext<PurchaseOrderContext>(context =>
            {
                purchaseOrder = new PurchaseOrder()
                {
                    Id = 1,
                    ClientId = 1,
                    Data = DateTime.UtcNow.Date,
                    Discount = 0,
                    Tax = 10,
                    BaseValue = 100,
                    TotalValue = 110,
                    PurchaseOrderProducts = new List<PurchaseOrderProduct>()
                    {
                        new PurchaseOrderProduct()
                        {
                            ProductId = 1,
                            Amount = 2,
                            UnitValue = 50.0m
                        },
                        new PurchaseOrderProduct()
                        {
                            ProductId = 2,
                            Amount = 5,
                            UnitValue = 10.0m
                        }
                    }
                };

                context.PurchaseOrders.Add(purchaseOrder);

                context.SaveChanges();
            });
        }

        [Fact]
        public async Task ShouldNotAllowNewOrderDuplicated()
        {
            var purchaseOrderService = Resolve<IPurchaseOrderService>();

            var newOrder = await purchaseOrderService.CreateNewPurchaseOrder(purchaseOrder);

            Assert.True(LocalNotification.HasNotification());

            // Assert
            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            var duplicatedMessage = localizationSource.GetString(GlobalizationKey.DuplicatePurchaseOrder);

            Assert.Contains(LocalNotification.GetAll(), w => w.DetailedMessage == GlobalizationKey.DuplicatePurchaseOrder.ToString());
            Assert.Contains(LocalNotification.GetAll(), w => w.Message == string.Format(duplicatedMessage, purchaseOrder.ClientId, purchaseOrder.Data));
        }

        [Fact]
        public async Task ShouldDeleteWithSucess()
        {
            var purchaseOrderService = Resolve<IPurchaseOrderService>();

            await purchaseOrderService.DeleteAsync(1);

            ServiceProvider.UsingDbContext<PurchaseOrderContext>(context =>
            {
                Assert.Equal(0, context.PurchaseOrders.Count());
            });
        }
    }
}
