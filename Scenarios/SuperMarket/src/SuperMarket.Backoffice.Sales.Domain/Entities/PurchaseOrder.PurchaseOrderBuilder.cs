using SuperMarket.Backoffice.Sales.Domain.Entities.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using Tnf.Builder;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.Sales.Domain.Entities
{
    public partial class PurchaseOrder
    {
        class PurchaseOrderBuilder : Builder<PurchaseOrder>,
            INewPurchaseOrderBuilder,
            INewPurchaseOrderBuilderLines,
            IUpdatePurchaseOrderBuilder,
            IUpdatePurchaseOrderBuilderLines
        {
            public PurchaseOrderBuilder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public PurchaseOrderBuilder(INotificationHandler notificationHandler, PurchaseOrder instance)
                : base(notificationHandler, instance)
            {
            }

            public PurchaseOrderBuilder GenerateNewPurchaseOrder()
            {
                Instance.Id = Guid.NewGuid();
                Instance.Date = DateTime.UtcNow;
                Instance.Number = Guid.NewGuid();
                Instance.Status = PurchaseOrderStatus.Processing;

                return this;
            }

            #region INewPurchaseOrderBuilder

            INewPurchaseOrderBuilder INewPurchaseOrderBuilder.UpdatePriceTable(PriceTable priceTable)
            {
                Instance.PriceTable = priceTable;
                return this;
            }

            INewPurchaseOrderBuilder INewPurchaseOrderBuilder.WithCustomer(Guid customerId)
            {
                Instance.CustomerId = customerId;
                return this;
            }

            INewPurchaseOrderBuilder INewPurchaseOrderBuilder.WithDiscount(decimal discount)
            {
                Instance.Discount = discount;
                return this;
            }

            INewPurchaseOrderBuilder INewPurchaseOrderBuilder.AddPurchaseOrderLines(Action<INewPurchaseOrderBuilderLines> lines)
            {
                lines(this);

                Instance.RecalculateBaseValue();

                return this;
            }

            INewPurchaseOrderBuilderLines INewPurchaseOrderBuilderLines.AddProduct(Guid productId, int quantity)
            {
                Instance.Lines.Add(new PurchaseOrderLine(productId, quantity));
                return this;
            }

            #endregion

            #region IUpdatePurchaseOrderBuilder

            IUpdatePurchaseOrderBuilder IUpdatePurchaseOrderBuilder.UpdatePriceTable(PriceTable priceTable)
            {
                Instance.PriceTable = priceTable;
                return this;
            }

            IUpdatePurchaseOrderBuilder IUpdatePurchaseOrderBuilder.WithDiscount(decimal discount)
            {
                Instance.Discount = discount;
                return this;
            }

            IUpdatePurchaseOrderBuilder IUpdatePurchaseOrderBuilder.UpdatePurchaseOrderLines(Action<IUpdatePurchaseOrderBuilderLines> lines)
            {
                lines(this);

                Instance.RecalculateBaseValue();

                return this;
            }

            IUpdatePurchaseOrderBuilderLines IUpdatePurchaseOrderBuilderLines.AddProduct(Guid productId, int quantity)
            {
                Instance.Lines.Add(new PurchaseOrderLine(productId, quantity));
                return this;
            }

            IUpdatePurchaseOrderBuilderLines IUpdatePurchaseOrderBuilderLines.UpdateProduct(Guid productId, int quantity)
            {
                Instance.Lines = Instance.Lines.RemoveAll(w => w.ProductId == productId).ToList();

                Instance.Lines.Add(new PurchaseOrderLine(productId, quantity));
                return this;
            }

            IUpdatePurchaseOrderBuilderLines IUpdatePurchaseOrderBuilderLines.RemoveProduct(Guid productId)
            {
                Instance.Lines = Instance.Lines.RemoveAll(w => w.ProductId == productId).ToList();
                return this;
            }

            #endregion

            protected override void Specifications()
            {
                var productsThatAreNotInThePriceTable = Instance.GetProductsThatAreNotInThePriceTable().JoinAsString(", ");

                AddSpecificationWithParamsForLocalizationKey<ProductsThatAreNotInThePriceTableSpecification>(productsThatAreNotInThePriceTable);

                var productsWhenHaveNegativeQuantity = Instance.GetProductsWhenHaveNegativeQuantity().JoinAsString(", ");

                AddSpecificationWithParamsForLocalizationKey<PurchaseOrderLineMustHaveValidQuantity>(productsWhenHaveNegativeQuantity);
            }
        }

        public interface INewPurchaseOrderBuilder : IBuilder<PurchaseOrder>
        {
            INewPurchaseOrderBuilder UpdatePriceTable(PriceTable priceTable);
            INewPurchaseOrderBuilder WithCustomer(Guid customerId);
            INewPurchaseOrderBuilder WithDiscount(decimal discount);
            INewPurchaseOrderBuilder AddPurchaseOrderLines(Action<INewPurchaseOrderBuilderLines> lines);
        }

        public interface INewPurchaseOrderBuilderLines
        {
            INewPurchaseOrderBuilderLines AddProduct(Guid productId, int quantity);
        }

        public interface IUpdatePurchaseOrderBuilder : IBuilder<PurchaseOrder>
        {
            IUpdatePurchaseOrderBuilder UpdatePriceTable(PriceTable priceTable);
            IUpdatePurchaseOrderBuilder WithDiscount(decimal discount);
            IUpdatePurchaseOrderBuilder UpdatePurchaseOrderLines(Action<IUpdatePurchaseOrderBuilderLines> lines);
        }

        public interface IUpdatePurchaseOrderBuilderLines
        {
            IUpdatePurchaseOrderBuilderLines AddProduct(Guid productId, int quantity);
            IUpdatePurchaseOrderBuilderLines UpdateProduct(Guid productId, int quantity);
            IUpdatePurchaseOrderBuilderLines RemoveProduct(Guid productId);
        }
    }
}
