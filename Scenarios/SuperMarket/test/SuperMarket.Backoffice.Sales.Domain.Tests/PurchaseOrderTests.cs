using SuperMarket.Backoffice.Sales.Domain.Entities;
using System;
using Xunit;

namespace SuperMarket.Backoffice.Sales.Domain.Tests
{

    public class PurchaseOrderTests : PurchaseOrderBaseClass
    {
        [Fact]
        public void CalculateBaseValueOfPurchaseOrder()
        {
            var purchaseOrderBuilder = PurchaseOrder.New(LocalNotification)
                .UpdatePriceTable(PriceTable)
                .WithCustomer(Guid.NewGuid())
                .WithDiscount(0)
                .AddPurchaseOrderLines(lines =>
                {
                    lines.AddProduct(Product1, 1); //10
                    lines.AddProduct(Product2, 2); //40
                    lines.AddProduct(Product3, 3); //90
                    lines.AddProduct(Product4, 4); //160
                });

            var purchaseOrder = purchaseOrderBuilder.Build();

            Assert.NotNull(purchaseOrder);

            Assert.Equal(300, purchaseOrder.BaseValue);

            var updatePurchaseOrderBuilder = PurchaseOrder.Update(LocalNotification, purchaseOrder)
                .UpdatePurchaseOrderLines(lines =>
                {
                    lines.RemoveProduct(Product1);
                    lines.RemoveProduct(Product2);
                    lines.UpdateProduct(Product3, 4); //120
                });

            purchaseOrder = purchaseOrderBuilder.Build();

            Assert.NotNull(purchaseOrder);

            Assert.Equal(280, purchaseOrder.BaseValue);
        }

        [Fact]
        public void CreateNewOrderNotifyWhenNotExistProductInPriceTable()
        {
            var purchaseOrderBuilder = PurchaseOrder.New(LocalNotification)
                .UpdatePriceTable(PriceTable)
                .WithCustomer(Guid.NewGuid())
                .WithDiscount(0)
                .AddPurchaseOrderLines(lines =>
                {
                    lines.AddProduct(Product1, 1);
                    lines.AddProduct(Product2, 2);
                    lines.AddProduct(Product3, 3);
                    lines.AddProduct(InvalidProduct, 4);
                });

            var pucharseOrder = purchaseOrderBuilder.Build();

            Assert.NotEmpty(LocalNotification.GetAll());

            Assert.Contains(LocalNotification.GetAll(), a => a.DetailedMessage == PurchaseOrder.Error.ProductsThatAreNotInThePriceTable.ToString());
        }

        [Fact]
        public void CreateNewOrderNotifyWhenProductHaveInvalidQuantity()
        {
            var purchaseOrderBuilder = PurchaseOrder.New(LocalNotification)
                .UpdatePriceTable(PriceTable)
                .WithCustomer(Guid.NewGuid())
                .WithDiscount(0)
                .AddPurchaseOrderLines(lines =>
                {
                    lines.AddProduct(Product1, 0);
                    lines.AddProduct(Product2, 0);
                });

            var pucharseOrder = purchaseOrderBuilder.Build();

            Assert.NotEmpty(LocalNotification.GetAll());

            Assert.Contains(LocalNotification.GetAll(), a => a.DetailedMessage == PurchaseOrder.Error.PurchaseOrderLineMustHaveValidQuantity.ToString());
            Assert.Contains(LocalNotification.GetAll(), a => a.Message.Contains(Product1.ToString()));
            Assert.Contains(LocalNotification.GetAll(), a => a.Message.Contains(Product2.ToString()));
        }

        [Fact]
        public void UpdateTaxPurchaseOrder()
        {
            var purchaseOrderBuilder = PurchaseOrder.New(LocalNotification)
                .UpdatePriceTable(PriceTable)
                .WithCustomer(Guid.NewGuid())
                .WithDiscount(10)
                .AddPurchaseOrderLines(lines =>
                {
                    lines.AddProduct(Product1, 1); //10
                    lines.AddProduct(Product2, 2); //40
                    lines.AddProduct(Product3, 3); //90
                    lines.AddProduct(Product4, 4); //160
                });

            var purchaseOrder = purchaseOrderBuilder.Build();

            Assert.NotNull(purchaseOrder);

            Assert.Equal(300, purchaseOrder.BaseValue);
            Assert.Equal(PurchaseOrder.PurchaseOrderStatus.Processing, purchaseOrder.Status);

            PurchaseOrder.UpdateTax(purchaseOrder, 50);

            Assert.Equal(340, purchaseOrder.TotalValue);
            Assert.Equal(PurchaseOrder.PurchaseOrderStatus.Completed, purchaseOrder.Status);
        }
    }
}
