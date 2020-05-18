using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Querying.Infra.Context;
using Querying.Infra.Entities;

namespace Querying.Web
{
    public class MigrationsHostedServices : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MigrationsHostedServices(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<PurchaseOrderContext>();

            await context.Database.MigrateAsync();

            await Seed(context);
        }

        private static async Task Seed(PurchaseOrderContext context)
        {
            if (await context.PurchaseOrders.AnyAsync())
            {
                return;
            }

            var date = DateTime.UtcNow.Date;
            var finalDate = date.AddDays(30);

            context.Products.Add(new Product("Sapato", 10.0m));
            context.Products.Add(new Product("Boné", 15.0m));
            context.Products.Add(new Product("Chinelo", 18.0m));
            context.Products.Add(new Product("Calça", 25.0m));
            context.Products.Add(new Product("Tenis", 35.0m));

            await context.SaveChangesAsync();

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

                order.ProductOrders.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = context.Products.First(w => w.Id == productId),
                    UnitValue = context.Products.First(w => w.Id == productId).Value
                });

                productId = productRandom.Next(1, 5);
                quant = productAmountRandom.Next(1, 4);

                order.ProductOrders.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = context.Products.First(w => w.Id == productId),
                    UnitValue = context.Products.First(w => w.Id == productId).Value
                });

                productId = productRandom.Next(1, 5);
                quant = productAmountRandom.Next(1, 4);

                order.ProductOrders.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = context.Products.First(w => w.Id == productId),
                    UnitValue = context.Products.First(w => w.Id == productId).Value
                });

                productId = productRandom.Next(1, 5);
                quant = productAmountRandom.Next(1, 4);

                order.ProductOrders.Add(new PurchaseOrderProduct()
                {
                    Quantity = quant,
                    Product = context.Products.First(w => w.Id == productId),
                    UnitValue = context.Products.First(w => w.Id == productId).Value
                });

                order.TotalValue = order.ProductOrders.Sum(s => s.UnitValue * s.Quantity);

                context.PurchaseOrders.Add(order);
            }

            await context.SaveChangesAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
