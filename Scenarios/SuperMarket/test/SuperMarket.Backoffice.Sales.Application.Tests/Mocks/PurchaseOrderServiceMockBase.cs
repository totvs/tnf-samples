using SuperMarket.Backoffice.Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.Sales.Application.Tests.Mocks
{
    public class PurchaseOrderServiceMockManager
    {
        private readonly INotificationHandler _notification;

        public Guid PurchaseOrderGuid { get; private set; }
        public Guid ValidProduct { get; private set; }

        public List<PurchaseOrder> List = new List<PurchaseOrder>();
        public PriceTable PriceTable;

        public PurchaseOrderServiceMockManager(INotificationHandler _notificationHandler)
        {
            _notification = _notificationHandler;

            PopulateList();
        }

        private void PopulateList()
        {
            var product1 = Guid.NewGuid();

            ValidProduct = product1;

            var product2 = Guid.NewGuid();
            var product3 = Guid.NewGuid();
            var product4 = Guid.NewGuid();

            PriceTable = PriceTable.CreateTable(new Dictionary<Guid, decimal>()
            {
                { product1, 10 },
                { product2, 20 },
                { product3, 30 },
                { product4, 40 },
            });

            var purchaseOrder = PurchaseOrder.New(_notification)
                .UpdatePriceTable(PriceTable)
                .WithCustomer(Guid.NewGuid())
                .WithDiscount(0)
                .AddPurchaseOrderLines(lines =>
                {
                    lines.AddProduct(product1, 1); //10
                    lines.AddProduct(product2, 2); //40
                    lines.AddProduct(product3, 3); //90
                    lines.AddProduct(product4, 4); //160
                })
                .Build();

            PurchaseOrderGuid = purchaseOrder.Id;

            List.Add(purchaseOrder);

            for (int i = 0; i < 3; i++)
            {
                List.Add(PurchaseOrder.New(_notification)
                    .UpdatePriceTable(PriceTable)
                    .WithCustomer(Guid.NewGuid())
                    .WithDiscount((i + 1) * 10)
                    .AddPurchaseOrderLines(lines =>
                    {
                        lines.AddProduct(product1, 1); //10
                        lines.AddProduct(product2, 2); //40
                        lines.AddProduct(product3, 3); //90
                        lines.AddProduct(product4, 4); //160
                    })
                    .Build());
            }
        }
    }
}
