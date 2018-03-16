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
    public class OrderServiceTests : TnfEfCoreIntegratedTestBase
    {
        private ILocalizationManager localizationManager;
        private Order order = null;

        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            services
                .AddDomainDependency()                                  // dependencia da camada Transactional.Domain
                .AddTnfEfCoreSqliteInMemory()                           // Configura o setup de teste para EntityFrameworkCore em memória
                .RegisterDbContextToSqliteInMemory<OrderContext>();     // Configura o cotexto a ser usado em memória pelo EntityFrameworkCore
        }

        protected override void PostInitialize(IServiceProvider provider)
        {
            base.PostInitialize(provider);

            provider.ConfigureTnf().ConfigureLocalization();

            localizationManager = provider.GetService<ILocalizationManager>();

            // Arrange
            provider.UsingDbContext<OrderContext>(context =>
            {
                order = new Order()
                {
                    Id = 1,
                    ClientId = 1,
                    Data = DateTime.UtcNow.Date,
                    Discount = 0,
                    Tax = 10,
                    BaseValue = 100,
                    TotalValue = 110,
                    Products = new List<ProductOrder>()
                    {
                        new ProductOrder()
                        {
                            ProductId = 1,
                            Amount = 2,
                            UnitValue = 50.0m
                        },
                        new ProductOrder()
                        {
                            ProductId = 2,
                            Amount = 5,
                            UnitValue = 10.0m
                        }
                    }
                };

                context.Orders.Add(order);

                context.SaveChanges();
            });
        }

        [Fact]
        public async Task ShouldNotAllowNewOrderDuplicated()
        {
            var orderService = Resolve<IOrderService>();

            var newOrder = await orderService.CreateNewOrder(order);

            Assert.True(LocalNotification.HasNotification());

            // Assert
            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            var duplicatedMessage = localizationSource.GetString(GlobalizationKey.DuplicateOrder);

            Assert.Contains(LocalNotification.GetAll(), w => w.DetailedMessage == GlobalizationKey.DuplicateOrder.ToString());
            Assert.Contains(LocalNotification.GetAll(), w => w.Message == string.Format(duplicatedMessage, order.ClientId, order.Data));
        }

        [Fact]
        public async Task ShouldDeleteWithSucess()
        {
            var orderService = Resolve<IOrderService>();

            await orderService.DeleteAsync(1);

            ServiceProvider.UsingDbContext<OrderContext>(context =>
            {
                Assert.Equal(0, context.Orders.Count());
            });
        }
    }
}
