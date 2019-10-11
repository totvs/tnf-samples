using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper.Infra.Context;
using Dapper.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Dapper.Web.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly PurchaseOrderContext _purchaseOrderContext;

        public MigrationHostedService(PurchaseOrderContext purchaseOrderContext)
        {
            _purchaseOrderContext = purchaseOrderContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _purchaseOrderContext.Database.MigrateAsync();

            var date = DateTime.UtcNow.Date;
            var finalDate = date.AddDays(30);

            _purchaseOrderContext.Products.Add(new Product("Sapato", 10.0m));
            _purchaseOrderContext.Products.Add(new Product("Boné", 15.0m));
            _purchaseOrderContext.Products.Add(new Product("Chinelo", 18.0m));
            _purchaseOrderContext.Products.Add(new Product("Calça", 25.0m));
            _purchaseOrderContext.Products.Add(new Product("Tenis", 35.0m));

            _purchaseOrderContext.SaveChanges();

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
                    Product = _purchaseOrderContext.Products.First(w => w.Id == productId),
                    UnitValue = _purchaseOrderContext.Products.First(w => w.Id == productId).Value
                });

                productId = productRandom.Next(1, 5);
                quant = productAmountRandom.Next(1, 4);

                order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = _purchaseOrderContext.Products.First(w => w.Id == productId),
                    UnitValue = _purchaseOrderContext.Products.First(w => w.Id == productId).Value
                });

                productId = productRandom.Next(1, 5);
                quant = productAmountRandom.Next(1, 4);

                order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = _purchaseOrderContext.Products.First(w => w.Id == productId),
                    UnitValue = _purchaseOrderContext.Products.First(w => w.Id == productId).Value
                });

                productId = productRandom.Next(1, 5);
                quant = productAmountRandom.Next(1, 4);

                order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = _purchaseOrderContext.Products.First(w => w.Id == productId),
                    UnitValue = _purchaseOrderContext.Products.First(w => w.Id == productId).Value
                });

                order.TotalValue = order.PurchaseOrderProducts.Sum(s => s.UnitValue * s.Quantity);

                _purchaseOrderContext.PurchaseOrders.Add(order);
            }

            _purchaseOrderContext.SaveChanges();
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
