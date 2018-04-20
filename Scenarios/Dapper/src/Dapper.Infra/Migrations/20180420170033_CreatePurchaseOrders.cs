using Dapper.Infra.Context.Migration;
using Dapper.Infra.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;

namespace Dapper.Infra.Migrations
{
    public partial class CreatePurchaseOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var contextFactory = new PurchaseOrderContextFactory();

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
                        Product = context.Products.First(w => w.Id == productId),
                        UnitValue = context.Products.First(w => w.Id == productId).Value
                    });

                    productId = productRandom.Next(1, 5);
                    quant = productAmountRandom.Next(1, 4);

                    order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                    {
                        Quantity = quant,
                        Product = context.Products.First(w => w.Id == productId),
                        UnitValue = context.Products.First(w => w.Id == productId).Value
                    });

                    productId = productRandom.Next(1, 5);
                    quant = productAmountRandom.Next(1, 4);

                    order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                    {
                        Quantity = quant,
                        Product = context.Products.First(w => w.Id == productId),
                        UnitValue = context.Products.First(w => w.Id == productId).Value
                    });

                    productId = productRandom.Next(1, 5);
                    quant = productAmountRandom.Next(1, 4);

                    order.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                    {
                        Quantity = quant,
                        Product = context.Products.First(w => w.Id == productId),
                        UnitValue = context.Products.First(w => w.Id == productId).Value
                    });

                    order.TotalValue = order.PurchaseOrderProducts.Sum(s => s.UnitValue * s.Quantity);

                    context.PurchaseOrders.Add(order);
                }

                context.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
