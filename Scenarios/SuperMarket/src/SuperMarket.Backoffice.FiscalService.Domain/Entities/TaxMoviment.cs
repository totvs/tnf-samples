using SuperMarket.Backoffice.FiscalService.Domain.Entities.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace SuperMarket.Backoffice.FiscalService.Domain.Entities
{
    public class TaxMoviment : Entity<Guid>
    {
        public Guid PurchaseOrderId { get; private set; }
        public decimal PurchaseOrderBaseValue { get; private set; }
        public decimal PurchaseOrderDiscount { get; private set; }
        public int Percentage { get; private set; }
        public decimal Tax { get; private set; }
        public decimal PurchaseOrderTotalValue { get; private set; }

        internal void RecalculateTaxTotalValue()
        {
            Tax = (Percentage / 100);

            PurchaseOrderTotalValue = (PurchaseOrderBaseValue - PurchaseOrderDiscount) + Tax;
        }

        public enum Error
        {
            TaxMovimentMustHaveOrderBaseValue,
            TaxMovimentMustHaveOrderDiscount,
            TaxMovimentMustHaveOrderId,
            TaxMovimentMustHaveOrderTax,
            TaxMovimentMustHaveOrderPercentage,
            TaxMovimentMustHaveOrderTotalValue
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
                Instance.Percentage = 10;

                Instance.RecalculateTaxTotalValue();

                return this;
            }

            protected override void Specifications()
            {
                base.Specifications();

                AddSpecification<TaxMovimentMustHaveOrderBaseValue>();
                AddSpecification<TaxMovimentMustHaveOrderDiscount>();
                AddSpecification<TaxMovimentMustHaveOrderId>();
                AddSpecification<TaxMovimentMustHaveOrderPercentage>();
                AddSpecification<TaxMovimentMustHaveOrderTax>();
                AddSpecification<TaxMovimentMustHaveOrderTotalValue>();
            }
        }
    }
}
