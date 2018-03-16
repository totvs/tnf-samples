using Microsoft.EntityFrameworkCore.Migrations;
using Querying.Infra.Context.Migration;
using Querying.Infra.Entities;
using System;
using System.Linq;

namespace Querying.Infra.Migrations
{
    public partial class IncludeOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var contextFactory = new OrderContextFactory();

            using (var context = contextFactory.CreateDbContext(Enumerable.Empty<string>().ToArray()))
            {
                var date = DateTime.UtcNow.Date;
                var finalDate = date.AddDays(30);

                context.Products.Add(new Product("Sapato", 10.0m));
                context.Products.Add(new Product("Boné", 15.0m));
                context.Products.Add(new Product("Chinelo", 18.0m));
                context.Products.Add(new Product("Calça", 25.0m));
                context.Products.Add(new Product("Tenis", 35.0m));

                context.SaveChanges();

                var productRandom = new Random();
                var productAmountRandom = new Random();

                for (var day = date.Date; day.Date <= finalDate.Date; day = day.AddDays(1))
                {
                    var order = new Order()
                    {
                        Date = day,
                        Customer = new Customer($"Customer {day}")
                    };

                    var productId = productRandom.Next(1, 5);
                    var amount = productAmountRandom.Next(1, 4);

                    order.ProductOrders.Add(new ProductOrder()
                    {
                        Amount = amount,
                        Product = context.Products.First(w => w.Id == productId),
                        UnitValue = context.Products.First(w => w.Id == productId).Value
                    });

                    productId = productRandom.Next(1, 5);
                    amount = productAmountRandom.Next(1, 4);

                    order.ProductOrders.Add(new ProductOrder()
                    {
                        Amount = amount,
                        Product = context.Products.First(w => w.Id == productId),
                        UnitValue = context.Products.First(w => w.Id == productId).Value
                    });

                    productId = productRandom.Next(1, 5);
                    amount = productAmountRandom.Next(1, 4);

                    order.ProductOrders.Add(new ProductOrder()
                    {
                        Amount = amount,
                        Product = context.Products.First(w => w.Id == productId),
                        UnitValue = context.Products.First(w => w.Id == productId).Value
                    });

                    productId = productRandom.Next(1, 5);
                    amount = productAmountRandom.Next(1, 4);

                    order.ProductOrders.Add(new ProductOrder()
                    {
                        Amount = amount,
                        Product = context.Products.First(w => w.Id == productId),
                        UnitValue = context.Products.First(w => w.Id == productId).Value
                    });

                    order.TotalValue = order.ProductOrders.Sum(s => s.UnitValue * s.Amount);

                    context.Orders.Add(order);
                }

                context.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
