using SuperMarket.FiscalService.Domain.Entities.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;

namespace SuperMarket.FiscalService.Domain.Entities
{
    public class TaxMoviment
    {
        private const int TaxPercentage = 10;

        public static TaxMovimentBuilder New(INotificationHandler notification)
            => new TaxMovimentBuilder(notification);

        public Guid Id { get; private set; }
        public Guid PurchaseOrderId { get; private set; }
        public decimal PurchaseOrderBaseValue { get; private set; }
        public decimal PurchaseOrderDiscount { get; private set; }
        public int Percentage { get; private set; }
        public decimal Tax { get; private set; }
        public decimal PurchaseOrderTotalValue { get; private set; }

        internal void RecalculateTaxTotalValue()
        {
            Tax = (Percentage / 100.0m);

            PurchaseOrderTotalValue = (PurchaseOrderBaseValue - PurchaseOrderDiscount) + Tax;
        }

        public enum Error
        {
            TaxMovimentMustHaveOrderBaseValue,
            TaxMovimentMustHaveOrderDiscount,
            TaxMovimentMustHaveOrderId
        }

        public class TaxMovimentBuilder : Builder<TaxMoviment>
        {
            public TaxMovimentBuilder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public TaxMovimentBuilder(INotificationHandler notificationHandler, TaxMoviment instance)
                : base(notificationHandler, instance)
            {
            }

            public TaxMovimentBuilder ForPurchaseOrder(Guid purchaseOrderId, decimal baseValue, decimal discount)
            {
                Instance.PurchaseOrderId = purchaseOrderId;
                Instance.PurchaseOrderBaseValue = baseValue;
                Instance.PurchaseOrderDiscount = discount;
                Instance.Percentage = TaxMoviment.TaxPercentage;

                Instance.RecalculateTaxTotalValue();

                return this;
            }

            protected override void Specifications()
            {
                base.Specifications();

                AddSpecification<TaxMovimentMustHaveOrderBaseValue>();
                AddSpecification<TaxMovimentMustHaveOrderDiscount>();
                AddSpecification<TaxMovimentMustHaveOrderId>();
            }
        }
    }
}
