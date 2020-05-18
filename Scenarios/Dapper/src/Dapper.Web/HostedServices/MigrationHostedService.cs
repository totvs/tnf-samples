using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper.Infra.Context;
using Dapper.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dapper.Web.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MigrationHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Criamos um escopo de service provider para pedir o PurchaseOrderContext dentro desse espoco.
            // Isso evita ficar com services que são IDisposable dentro do service provider raiz.
            using var scope = _serviceScopeFactory.CreateScope();
            var purchaseOrderContext = scope.ServiceProvider.GetService<PurchaseOrderContext>();

            await purchaseOrderContext.Database.MigrateAsync(cancellationToken);

            var date = DateTime.UtcNow.Date;
            var finalDate = date.AddDays(30);

            purchaseOrderContext.Products.Add(new Product("Sapato", 10.0m));
            purchaseOrderContext.Products.Add(new Product("Boné", 15.0m));
            purchaseOrderContext.Products.Add(new Product("Chinelo", 18.0m));
            purchaseOrderContext.Products.Add(new Product("Calça", 25.0m));
            purchaseOrderContext.Products.Add(new Product("Tenis", 35.0m));

            await purchaseOrderContext.SaveChangesAsync(cancellationToken);

            var productRandom = new Random();
            var productAmountRandom = new Random();

            for (var day = date.Date; day.Date <= finalDate.Date; day = day.AddDays(1))
            {
                var order = new PurchaseOrder()
                {
                    Date = day,
                    Customer = new Customer($"Customer {day}")
                };

                var productId = productRandom.Next(1, 5);
                var quant = productAmountRandom.Next(1, 4);

                order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = purchaseOrderContext.Products.First(w => w.Id == productId),
                    UnitValue = purchaseOrderContext.Products.First(w => w.Id == productId).Value
                });

                productId = productRandom.Next(1, 5);
                quant = productAmountRandom.Next(1, 4);

                order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = purchaseOrderContext.Products.First(w => w.Id == productId),
                    UnitValue = purchaseOrderContext.Products.First(w => w.Id == productId).Value
                });

                productId = productRandom.Next(1, 5);
                quant = productAmountRandom.Next(1, 4);

                order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = purchaseOrderContext.Products.First(w => w.Id == productId),
                    UnitValue = purchaseOrderContext.Products.First(w => w.Id == productId).Value
                });

                productId = productRandom.Next(1, 5);
                quant = productAmountRandom.Next(1, 4);

                order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = purchaseOrderContext.Products.First(w => w.Id == productId),
                    UnitValue = purchaseOrderContext.Products.First(w => w.Id == productId).Value
                });

                order.TotalValue = order.PurchaseOrderProducts.Sum(s => s.UnitValue * s.Quantity);

                purchaseOrderContext.PurchaseOrders.Add(order);
            }

            await purchaseOrderContext.SaveChangesAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
